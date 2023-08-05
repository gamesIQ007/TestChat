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

            RectTransform rectTransform = m_BgImage.GetComponent<RectTransform>();

            RectTransform parentRectTransform = m_BgImage.transform.parent.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(parentRectTransform.rect.width, rectTransform.anchoredPosition.y);

            rectTransform.sizeDelta = new Vector2(parentRectTransform.rect.width, rectTransform.sizeDelta.y);
            m_BgImage.rectTransform.anchoredPosition = new Vector2(1509 - (m_Text.text.Length * 15), 0);
        }

        /// <summary>
        /// Задать стиль чужих сообщений
        /// </summary>
        public void SetStyleBySender()
        {
            m_BgImage.color = m_BgColorForSender;
            m_Text.alignment = TextAnchor.MiddleLeft;

            RectTransform rectTransform = m_BgImage.GetComponent<RectTransform>();

            RectTransform parentRectTransform = m_BgImage.transform.parent.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);

            rectTransform.sizeDelta = new Vector2(parentRectTransform.rect.width, rectTransform.sizeDelta.y);
            m_BgImage.rectTransform.anchoredPosition = new Vector2((m_Text.text.Length * 15) - 1509, 0);
        }

        public void SetBGSize()
        {
            Debug.Log(m_Text.text.Length + " " + m_BgImage.rectTransform.parent.position);
            //m_BgImage.rectTransform.anchoredPosition = new Vector2(1509 - (m_Text.text.Length * 15), 0);
            //m_Text.rectTransform.sizeDelta = new Vector2(m_Text.text.Length * 20, 50);
            //m_BgImage.rectTransform.sizeDelta = new Vector2(m_Text.text.Length * 20, 50);
            //m_Text.rectTransform.
        }
    }
}