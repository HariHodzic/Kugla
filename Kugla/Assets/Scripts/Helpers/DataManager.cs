using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public enum OrderBy
    {
        ASC,
        DESC
    }

    public class DataManager
    {
        public static void AppendList<T>(T obj, string key, string orderByProp = "", OrderBy orderBy = OrderBy.DESC)
        {
            var allRecordsFromStore = PlayerPrefs.GetString(key);
            var allRecs = new List<T>();

            if (!string.IsNullOrWhiteSpace(allRecordsFromStore) && allRecordsFromStore != "{}")
            {
                allRecs = JsonConvert.DeserializeObject<List<T>>(allRecordsFromStore);
            }

            allRecs.Add(obj);

            if (!string.IsNullOrWhiteSpace(orderByProp) && allRecs.Count > 1)
            {
                var prop = typeof(T).GetProperty(orderByProp);

                if (prop != null)
                {
                    allRecs = orderBy == OrderBy.ASC ? allRecs.OrderBy(x => prop).ToList() : allRecs.OrderByDescending(x => prop).ToList();
                }
            }

            var scoresToSave = JsonConvert.SerializeObject(allRecs);
            PlayerPrefs.SetString(key, scoresToSave);
            PlayerPrefs.Save();
        }

        public static void AddSerializedToJson<T>(T obj, string key)
        {
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(obj));
            PlayerPrefs.Save();
        }

        public static T GetDeserializedFromJson<T>(string key) => JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
    }
}