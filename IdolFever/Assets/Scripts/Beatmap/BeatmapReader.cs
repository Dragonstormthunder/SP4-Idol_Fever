using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace IdolFever.Beatmap
{



    public class MidiEvent
    {
        public ulong tickPos;
        public uint[] eventData;

        public MidiEvent(ulong pos, uint[] data)
        {
            tickPos = pos;
            eventData = data;
        }
        public static int CompareEvents(MidiEvent x, MidiEvent y)
        {
            if (x.tickPos == y.tickPos)
            {
                if (x.eventData[0] == 0xFF51 && y.eventData[0] != 0xFF51) return -1;
                else if (x.eventData[0] != 0xFF51 && y.eventData[0] == 0xFF51) return 1;
            }
            return (int)(x.tickPos - y.tickPos);
        }
    }

    public class BeatmapReader
    {
        private uint ReadVLInt(BinaryReader br, out uint o)
        {
            o = 0;
            uint bytes = 0;
            while (true)
            {
                ++bytes;
                byte m = br.ReadByte();
                o = (o << 7) | (m & 127u);
                if (m < 128) break;
            }
            return bytes;
        }

        public Beatmap ReadBeatmap(string fn)
        {
            FileStream fs = File.OpenRead(fn);
            BinaryReader br = new BinaryReader(fs);
            List<MidiEvent> events = new List<MidiEvent>();

            br.ReadChars(4);
            uint hlen = br.ReadUInt32();
            br.ReadUInt16();
            uint nchunks = br.ReadUInt16();
            uint ppqn = br.ReadUInt16();

            for (int i = 0; i < nchunks; ++i)
            {
                br.ReadChars(4);
                uint tlen = br.ReadUInt32();
                uint nbytes = 0;
                uint time = 0;
                byte mev = 0;
                while (true)
                {
                    uint dt;
                    nbytes += ReadVLInt(br, out dt);
                    time += dt;
                    byte code = br.ReadByte();
                    List<uint> ev = new List<uint>();
                    if (code < 0x80)
                    {
                        switch (mev >> 4)
                        {
                            case 8:
                            case 9:
                                if (br.ReadByte() == 0)
                                    ev.Add((mev & 15u) | 0x80u);
                                else ev.Add(mev);
                                ev.Add(code);
                                break;
                            case 10:
                            case 11:
                            case 14:
                                br.ReadByte();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (code < 0xF0)
                    {
                        mev = code;
                        switch (code >> 4)
                        {
                            case 8:
                            case 9:
                                byte note = br.ReadByte();
                                if (br.ReadByte() == 0)
                                    ev.Add((code & 15u) | 0x80u);
                                else ev.Add(code);
                                ev.Add(note); 
                                break;
                            case 10:
                            case 11:
                            case 14:
                                br.ReadByte();
                                br.ReadByte();
                                break;
                            case 12:
                            case 13:
                                br.ReadByte();
                                break;


                            default:
                                break;
                        }
                    }
                    else if (code == 0xFF)
                    {
                        byte fftype = br.ReadByte();
                        nbytes += 1;
                        if (fftype >= 1 && fftype <= 9)
                        {
                            uint fflen;
                            ReadVLInt(br, out fflen);
                            br.ReadChars((int)fflen);
                        }
                        else if (fftype == 0x2F)
                        {
                            br.ReadByte();
                            break;
                        }
                        else if (fftype == 0x51)
                        {
                            uint m = br.ReadUInt32() & 0xFFFFFFu;
                            ev.Add(0xFF51);
                            ev.Add(m);
                        }
                        else if(fftype == 0x58)
                        {
                            br.ReadBytes(5);
                        }
                    }
                    if(ev.Count > 0)
                    {
                        events.Add(new MidiEvent(time, ev.ToArray()));
                    }
                }
            }
            events.Sort(MidiEvent.CompareEvents);
            Beatmap b = new Beatmap();
            ulong ltime = 0;
            ulong usec = 0;
            ulong usecPerBeat = 500000;
            ulong[] holdBeats = new ulong[4];
            foreach(MidiEvent m in events)
            {
                ulong mm = usec;
                usec += ((m.tickPos - ltime) * usecPerBeat) / ppqn;
                ltime = mm;
                if (m.eventData[0] == 0x90)
                {
                    switch (m.eventData[1])
                    {
                        case 0x33:
                            b.beats.Add(new BeatmapEvent(usec, 0, NoteKey.KEY1));
                            break;
                        case 0x32:
                            b.beats.Add(new BeatmapEvent(usec, 0, NoteKey.KEY2));
                            break;
                        case 0x31:
                            b.beats.Add(new BeatmapEvent(usec, 0, NoteKey.KEY3));
                            break;
                        case 0x30:
                            b.beats.Add(new BeatmapEvent(usec, 0, NoteKey.KEY4));
                            break;
                    }
                }
                if (m.eventData[0] == 0x98)
                {
                    switch (m.eventData[1])
                    {
                        case 0x33:
                            holdBeats[0] = usec;
                            break;
                        case 0x32:
                            holdBeats[1] = usec;
                            break;
                        case 0x31:
                            holdBeats[2] = usec;
                            break;
                        case 0x30:
                            holdBeats[3] = usec;
                            break;
                    }
                }
                if (m.eventData[0] == 0x88)
                {
                    switch (m.eventData[1])
                    {
                        case 0x33:
                            b.beats.Add(new BeatmapEvent(holdBeats[0], usec - holdBeats[0], NoteKey.KEY1));
                            break;
                        case 0x32:
                            b.beats.Add(new BeatmapEvent(holdBeats[1], usec - holdBeats[1], NoteKey.KEY2));
                            break;
                        case 0x31:
                            b.beats.Add(new BeatmapEvent(holdBeats[2], usec - holdBeats[2], NoteKey.KEY3));
                            break;
                        case 0x30:
                            b.beats.Add(new BeatmapEvent(holdBeats[3], usec - holdBeats[3], NoteKey.KEY4));
                            break;
                    }
                }
                if(m.eventData[0] == 0xFF51)
                {
                    usecPerBeat = m.eventData[1];
                }
            }
        }
    }
}
