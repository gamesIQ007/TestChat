using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace NetworkChat
{
    /// <summary>
    /// Список пользователей
    /// </summary>
    public class UserList : NetworkBehaviour
    {
		/// <summary>
		/// Синглтон
		/// </summary>
		public static UserList Instance;

		/// <summary>
		/// Событие при обновлении списка пользователей
		/// </summary>
		public static UnityAction<List<UserData>> UpdateUserList;

        /// <summary>
        /// Список всех пользователей
        /// </summary>
        [SerializeField] private List<UserData> m_AllUsersData = new List<UserData>();


        private void Awake()
        {
			Instance = this;
        }


        public override void OnStartClient()
        {
            base.OnStartClient();

			m_AllUsersData.Clear();
        }


        /// <summary>
        /// Команда на сервер добавить пользователя в список
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userNickname">Ник пользователя</param>
        [Server]
		public void SvAddCurrentUser(int userId, string userNickname)
		{
			UserData data = new UserData(userId, userNickname);

			m_AllUsersData.Add(data);

			if (isServerOnly)
			{
				RpcClearUserDataList();
			}

			for (int i = 0; i < m_AllUsersData.Count; i++)
            {
				RpcAddCurrentUser(m_AllUsersData[i].ID, m_AllUsersData[i].Nickname);
			}
		}

		/// <summary>
		/// Команда на сервер удалить пользователя из списка
		/// </summary>
		/// <param name="userId">ID пользователя</param>
		[Server]
		public void SvRemoveCurrentUser(int userId)
		{
			for (int i = 0; i < m_AllUsersData.Count; i++)
			{
				if (m_AllUsersData[i].ID == userId)
                {
					m_AllUsersData.RemoveAt(i);
					break;
                }
			}

			RpcRemoveCurrentUser(userId);
		}

		/// <summary>
		/// Клиент, очистить список пользователей
		/// </summary>
		[ClientRpc]
		private void RpcClearUserDataList()
		{
			m_AllUsersData.Clear();
		}

		/// <summary>
		/// Клиент, добавить пользователя в список
		/// </summary>
		/// <param name="userId">ID пользователя</param>
		/// <param name="userNickname">Ник пользователя</param>
		[ClientRpc]
		private void RpcAddCurrentUser(int userId, string userNickname)
		{
			if (isClient && isServer)
            {
				UpdateUserList?.Invoke(m_AllUsersData);
				return;
            }

			UserData data = new UserData(userId, userNickname);

			m_AllUsersData.Add(data);

			UpdateUserList?.Invoke(m_AllUsersData);
		}

		/// <summary>
		/// Клиент, удалить пользователя из списка
		/// </summary>
		/// <param name="userId">ID пользователя</param>
		[ClientRpc]
		private void RpcRemoveCurrentUser(int userId)
		{
			for (int i = 0; i < m_AllUsersData.Count; i++)
			{
				if (m_AllUsersData[i].ID == userId)
				{
					m_AllUsersData.RemoveAt(i);
					break;
				}
			}

			UpdateUserList?.Invoke(m_AllUsersData);
		}
	}
}