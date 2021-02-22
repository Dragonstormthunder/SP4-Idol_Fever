using UnityEngine;
using TwitchChatConnect.Client;
using TwitchChatConnect.Config;
using TwitchChatConnect.Data;

namespace IdolFever {
    internal sealed class TwitchConnect: MonoBehaviour {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Unity User Callback Event Funcs

        private void Start() {
            TwitchChatClient.instance.Init(() => {
                    TwitchChatClient.instance.onChatMessageReceived += OnChatMessageReceived;
                    TwitchChatClient.instance.onChatCommandReceived += OnChatCommandReceived;
                    TwitchChatClient.instance.onChatRewardReceived += OnChatRewardReceived;
                },
                message => {
                    Debug.LogError(message, this);
                }
            );
        }

        #endregion

        private void OnChatMessageReceived(TwitchChatMessage chatMessage) {
            string msg = $"{chatMessage.User.DisplayName} (bits: {chatMessage.Bits}, isSub: {chatMessage.User.IsSub}, ID: {chatMessage.User.Id}): {chatMessage.Message}";
            DoSthWithText(msg);
        }

        private void OnChatCommandReceived(TwitchChatCommand chatCommand) {
            TwitchConnectData data = ScriptableObject.CreateInstance<TwitchConnectData>(); //??
            string myParams = string.Join(" - ", chatCommand.Parameters);
            string message = $"Command: '{chatCommand.Command}' - Username: {chatCommand.User.DisplayName} - Bits: {chatCommand.Bits} - Sub: {chatCommand.User.IsSub} - Parameters: {myParams}";

            TwitchChatClient.instance.SendChatMessage($"Hello {chatCommand.User.DisplayName}! I received your message.");
            TwitchChatClient.instance.SendChatMessage($"Hello {chatCommand.User.DisplayName}! This message will be sent in 5 seconds.", 5);

            DoSthWithText(message);
        }

        private void OnChatRewardReceived(TwitchChatReward chatReward) {
            string message = $"Reward unlocked by {chatReward.User.DisplayName} - Reward ID: {chatReward.CustomRewardId} - ID: {chatReward.User.Id} - Message: {chatReward.Message}";
            DoSthWithText(message);
        }

        private void DoSthWithText(string msg) {
            Debug.Log(msg, this);
        }
    }
}