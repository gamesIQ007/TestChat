using UnityEngine;
using UnityEngine.UI;

namespace NetworkChat
{
    /// <summary>
    /// Поле ввода сообщения
    /// </summary>
    public class UIMessageInputField : MonoBehaviour
    {
        /// <summary>
        /// Синглтон
        /// </summary>
        public static UIMessageInputField Instance;

        /// <summary>
        /// Поле ввода сообщения
        /// </summary>
        [SerializeField] private InputField m_MessageInputField;

        /// <summary>
        /// Поле ввода ника
        /// </summary>
        [SerializeField] private InputField m_NicknameInputField;

        /// <summary>
        /// Пустое ли поле ввода
        /// </summary>
        public bool IsEmpty => m_MessageInputField.text == "";


        private void Awake()
        {
            Instance = this;
        }


        /// <summary>
        /// Получить строку
        /// </summary>
        /// <returns>Строка</returns>
        public string GetString()
        {
            return m_MessageInputField.text;
        }

        /// <summary>
        /// Получить ник
        /// </summary>
        /// <returns>Ник</returns>
        public string GetNickname()
        {
            return m_NicknameInputField.text;
        }

        /// <summary>
        /// Очистить строку
        /// </summary>
        public void ClearString()
        {
            m_MessageInputField.text = "";
        }

        /// <summary>
        /// Отправить сообщение
        /// </summary>
        public void SendMessageToChat()
        {
            User.Local.SendMessageToChat();
        }

        /// <summary>
        /// Подключиться к чату
        /// </summary>
        public void JoinToChat()
        {
            User.Local.JoinToChat();
        }
    }
}