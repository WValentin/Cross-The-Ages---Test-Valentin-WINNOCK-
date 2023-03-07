using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class UsersFetching : MonoBehaviour
{
    [SerializeField]
    private string apiUrl = "https://random-data-api.com/api/users/random_user?size=5";

    [SerializeField]
    private List<TMP_Text> userDataTexts;

    void Start()
    {
        // Trust all SSL certificates
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

        StartCoroutine(FetchJsonData());
    }

    IEnumerator FetchJsonData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);

            yield return request.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                try
            {
                throw new WebException(webRequest.error);
            }
            catch (WebException e)
            {
                Debug.LogError(e.Message);
            }

            yield break;
            }
            else
            {
                // If the data was successfully fetched, parse it into a C# object
                string json = request.downloadHandler.text;
                UserDataList usersData = JsonUtility.FromJson<UserDataList>(json);
                int i = 0;

                foreach (UserData userData in usersData.data)
                {
                    userDataTexts[i].text = userData.id + "\n" + userData.first_name + "\n" + userData.last_name + "\n" + userData.email;
                    if (i == 4)
                        break;
                    i++;
                }
            }
        }
    }

    public void DeleteData(GameObject userData)
    {
        Destroy(userData);
    }


    // class that corresponds to the JSON data of a user
    [System.Serializable]
    private class UserData
    {
        public int id;
        public string first_name;
        public string last_name;
        public string email;
    }

    [System.Serializable]
    private class UserDataList
    {
        public UserData[] data;
    }
}
