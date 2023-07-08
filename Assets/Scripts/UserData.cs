using UnityEngine;
using Mirror;

namespace NetworkChat
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public class UserData : MonoBehaviour
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
    /// Сериализация/десериализация данных пользователя
    /// </summary>
    public static class UserDateWriteRead
    {
        /// <summary>
        /// Сериализация данных пользователя
        /// </summary>
        /// <param name="writer">Писец</param>
        /// <param name="data">Данные пользователя</param>
        public static void WriteUserData(this NetworkWriter writer, UserData data)
        {
            writer.WriteInt(data.ID);
            writer.WriteString(data.Nickname);
        }

        /// <summary>
        /// Десериализация данных пользователя
        /// </summary>
        /// <param name="reader">Чтец</param>
        /// <param name="data">Данные пользователя</param>
        /// <returns>Данные пользователя</returns>
        public static UserData ReadUserData(this NetworkReader reader)
        {
            return new UserData(reader.ReadInt(), reader.ReadString());
        }
    }
}