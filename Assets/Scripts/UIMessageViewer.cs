using System.Collections.Generic;
using UnityEngine;

namespace NetworkChat
{
    /// <summary>
    /// Отображение сообщений
    /// </summary>
    public class UIMessageViewer : MonoBehaviour
    {
        /// <summary>
        /// Блок с сообщением
        /// </summary>
        [SerializeField] private UIMessageBox m_MessageBox;
        /// <summary>
        /// Панель сообщений
        /// </summary>
        [SerializeField] private Transform m_MessagePanel;


        /// <summary>
        /// Блок с пользователями
        /// </summary>
        [SerializeField] private UIMessageBox m_UserBox;
        /// <summary>
        /// Панель пользователей
        /// </summary>
        [SerializeField] private Transform m_UserListPanel;


        #region Unity Events

        private void Start()
        {
            User.ReceiveMessageToChat += OnReceiveMessageToChat;
            UserList.UpdateUserList += OnUpdateUserList;
        }

        private void OnDestroy()
        {
            User.ReceiveMessageToChat -= OnReceiveMessageToChat;
            UserList.UpdateUserList -= OnUpdateUserList;
        }

        #endregion


        private void OnReceiveMessageToChat(UserData data, string message)
        {
            AppendMessage(data, message);
        }

        private void OnUpdateUserList(List<UserData> userList)
        {
            for (int i = 0; i < m_UserListPanel.childCount; i++)
            {
                Destroy(m_UserListPanel.GetChild(i).gameObject);
            }

            for (int i = 0; i < userList.Count; i++)
            {
                UIMessageBox userBox = Instantiate(m_UserBox);

                userBox.transform.SetParent(m_UserListPanel);
                userBox.SetText(userList[i].Nickname);
                userBox.transform.localScale = Vector3.one;
            }
        }


        /// <summary>
        /// Опубликовать сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        private void AppendMessage(UserData data, string message)
        {
            UIMessageBox messageBox = Instantiate(m_MessageBox);

            messageBox.transform.SetParent(m_MessagePanel);
            messageBox.SetText(data.Nickname + ": " + message);
            messageBox.transform.localScale = Vector3.one;

            if (data.ID == User.Local.Data.ID)
            {
                messageBox.SetStyleBySelf();
            }
            else
            {
                messageBox.SetStyleBySender();
            }

            messageBox.SetBGSize();
        }
    }
}