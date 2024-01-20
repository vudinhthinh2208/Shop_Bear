using Newtonsoft.Json;

namespace Shop_Bear.Repository
{
	public static class SessionExtensions
	{
		public static void Set(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T Get<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key);
			return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
		}
	}
}
