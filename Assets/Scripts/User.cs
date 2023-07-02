using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace NetworkChat
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    [System.Serializable]
    public class UserData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID;
        /// <summary>
        /// Ник
        /// </summary>
        public string Nickname;

        public UserData(int id, string nickname)
        {
            ID = id;
            Nickname = nickname;
        }
    }

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : NetworkBehaviour
    {
        /// <summary>
        /// Локальный пользователь
        /// </summary>
        public static User Local
        {
            get
            {
                var x = NetworkClient.localPlayer;

                if (x != null)
                {
                    return x.GetComponent<User>();
                }

                return null;
            }
        }

        /// <summary>
        /// Получено сообщение из чата
        /// </summary>
        public static UnityAction<int, string> ReceiveMessageToChat;

        /// <summary>
        /// Данные пользователя
        /// </summary>
        private UserData m_Data;
        public UserData Data => m_Data;

        /// <summary>
        /// Поле ввода сообщения
        /// </summary>
        private UIMessageInputField m_UIMessageInputField;


        #region Unity Events

        private void Start()
        {
            m_UIMessageInputField = UIMessageInputField.Instance;

            m_Data = new UserData((int) netId, "Nickname");
        }

        private void Update()
        {
            if (isOwned == false) return;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat();
            }
        }

        #endregion


        public override void OnStopServer()
        {
            base.OnStopServer();

            UserList.Instance.SvRemoveCurrentUser(m_Data.ID);
        }


        // Подключение

        /// <summary>
        /// Подключиться к чату
        /// </summary>
        public void JoinToChat()
        {
            m_Data.Nickname = m_UIMessageInputField.GetNickname();

            CmdAddUser(m_Data.ID, m_Data.Nickname);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [Command]
        private void CmdAddUser(int userId, string nickname)
        {
            UserList.Instance.SvAddCurrentUser(userId, nickname);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Command]
        private void CmdRemoveUser(int userId)
        {
            UserList.Instance.SvRemoveCurrentUser(userId);
        }


        // Чат

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        public void SendMessageToChat()
        {
            if (isOwned == false) return;

            if (m_UIMessageInputField.IsEmpty) return;

            CmdSendMessageToChat(m_Data.ID, m_UIMessageInputField.GetString());

            m_UIMessageInputField.ClearString();
        }

        /// <summary>
        /// Команда на сервер отправить сообщение в чат
        /// </summary>
        /// <param name="message">Сообщение</param>
        [Command]
        private void CmdSendMessageToChat(int userId, string message)
        {
            Debug.Log($"User send message to server. Message: {message}");

            SvPostMessage(userId, message);
        }

        /// <summary>
        /// Сервер получение сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        [Server]
        private void SvPostMessage(int userId, string message)
        {
            Debug.Log($"Server received message by user. Message: {message}");

            RpcReceiveMessage(userId, message);
        }

        /// <summary>
        /// Клиент получение сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        [ClientRpc]
        private void RpcReceiveMessage(int userId, string message)
        {
            Debug.Log($"User received message. Message: {message}");

            ReceiveMessageToChat?.Invoke(userId, message);
        }
    }
}