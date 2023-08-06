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
		public List<UserData> AllUsersData => m_AllUsersData;


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
		public void SvAddCurrentUser(UserData data)
		{
			m_AllUsersData.Add(data);

			if (isServerOnly)
			{
				RpcClearUserDataList();
			}

			for (int i = 0; i < m_AllUsersData.Count; i++)
            {
				RpcAddCurrentUser(m_AllUsersData[i]);
			}
		}

		/// <summary>
		/// Команда на сервер удалить пользователя из списка
		/// </summary>
		/// <param name="userId">ID пользователя</param>
		[Server]
		public void SvRemoveCurrentUser(UserData data)
		{
			for (int i = 0; i < m_AllUsersData.Count; i++)
			{
				if (m_AllUsersData[i].ID == data.ID)
                {
					m_AllUsersData.RemoveAt(i);
					break;
                }
			}

			RpcRemoveCurrentUser(data);
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
		private void RpcAddCurrentUser(UserData data)
		{
			if (isClient && isServer)
            {
				UpdateUserList?.Invoke(m_AllUsersData);
				return;
            }

			m_AllUsersData.Add(data);

			UpdateUserList?.Invoke(m_AllUsersData);
		}

		/// <summary>
		/// Клиент, удалить пользователя из списка
		/// </summary>
		/// <param name="userId">ID пользователя</param>
		[ClientRpc]
		private void RpcRemoveCurrentUser(UserData data)
		{
			for (int i = 0; i < m_AllUsersData.Count; i++)
			{
				if (m_AllUsersData[i].ID == data.ID)
				{
					m_AllUsersData.RemoveAt(i);
					break;
				}
			}

			UpdateUserList?.Invoke(m_AllUsersData);
		}
	}
}