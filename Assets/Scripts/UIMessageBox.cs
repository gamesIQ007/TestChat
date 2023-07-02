using UnityEngine;
using UnityEngine.UI;

namespace NetworkChat
{
    /// <summary>
    /// Блок сообщения
    /// </summary>
    public class UIMessageBox : MonoBehaviour
    {
        /// <summary>
        /// Текст
        /// </summary>
        [SerializeField] private Text m_Text;

        /// <summary>
        /// Фоновое изображение сообщения
        /// </summary>
        [SerializeField] private Image m_BgImage;

        /// <summary>
        /// Цвет своих сообщений
        /// </summary>
        [SerializeField] private Color m_BgColorForSelf;
        /// <summary>
        /// Цвет чужих сообщений
        /// </summary>
        [SerializeField] private Color m_BgColorForSender;


        /// <summary>
        /// Задать текст
        /// </summary>
        /// <param name="text">Текст</param>
        public void SetText(string text)
        {
            m_Text.text = text;
        }

        /// <summary>
        /// Задать стиль своих сообщений
        /// </summary>
        public void SetStyleBySelf()
        {
            m_BgImage.color = m_BgColorForSelf;
            m_Text.alignment = TextAnchor.MiddleRight;
        }

        /// <summary>
        /// Задать стиль чужих сообщений
        /// </summary>
        public void SetStyleBySender()
        {
            m_BgImage.color = m_BgColorForSender;
            m_Text.alignment = TextAnchor.MiddleLeft;
        }
    }
}