using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyMentor.Accounts;
using Xamarin.Forms;

namespace MyMentor.Services
{
    public static class ApiConstants
    {
        public static string AccountController = "Account";
        public static string AcademicInterestsController = "AcademicInterests";
        public static string StudentMentorController = "StudentMentor";
        public static string MessagingController = "Messaging";


        //public static string ApiUrl = Device.RuntimePlatform == Device.Android ? "https://10.0.2.2:5001/api/" : "https://localhost:5001/api/";
         public static string ApiUrl = "http://www.jab-software.com/api/";
    }
    public interface IRestService
    {
        Task<bool> IsValidUserAsync(string username, string password);
    }
    public class RestService : IRestService
    {
        private HttpClient _client;
        public RestService()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
            _client = new HttpClient(GetInsecureHandler());
        }
        public HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        public async Task<UserInfo> LoginUser(string username, string password)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.AccountController}/user/{username}/login/{password}");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
                return userInfo;
            }
            return new UserInfo();
        }
        public async Task<IEnumerable<AcademicInterest>> GetAcademicInterests()
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.AcademicInterestsController}");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<AcademicInterest>>(json);
                return items;
            }
            return new List<AcademicInterest>();
        }

        public async Task<IEnumerable<UserInfo>> GetMentorsForAcademicInterest(Guid academicInterestId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.StudentMentorController}/{academicInterestId}/getMentors");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                return items;
            }
            return new List<UserInfo>();
        }
        public async Task<IEnumerable<DirectMessage>> GetAllMessagesForUser(string userId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.MessagingController}/{userId}/getMessages");
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<DirectMessage>>(json);
                return items;
            }
            return new List<DirectMessage>();
        }
        public async Task SendMessage(DirectMessage message)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.MessagingController}/sendMessage/{message.SenderId}/{message.RecipientId}/{message.Message}");
            await _client.GetAsync(uri);
        }
        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var json = JsonConvert.SerializeObject(new { username = username, password = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.AccountController}/login");
            var response = await _client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        //public async Task<IEnumerable<AcademicInterest>> GetAcademicInterestsForUser(string username)
        //{
        //    var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.AcademicInterestsController}/forUser/{username}");
        //    var response = await _client.GetAsync(uri);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var items = JsonConvert.DeserializeObject<List<AcademicInterest>>(json);
        //        return items;
        //    }
        //    else
        //    {
        //        return new List<AcademicInterest>();
        //    }
        //}

        public async Task<List<UserInfo>> GetMentorsForStudent(string studentUserId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.StudentMentorController}/{studentUserId}/mentors");
            var response = await _client.GetAsync(uri);
            // return the list of mentors if the request was successful
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var mentorsForStudent = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                return mentorsForStudent;
            }
            return new List<UserInfo>();
        }

        public async Task<List<UserInfo>> GetStudentsForMentor(string mentorUserId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.StudentMentorController}/{mentorUserId}/mentorships");
            var response = await _client.GetAsync(uri);
            // return the list of mentors if the request was successful
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var studentsForMentor = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                return studentsForMentor;
            }
            return new List<UserInfo>();
        }

        public async Task<UserInfo> RequestMentorship(string mentorUserId, string studentUserId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.StudentMentorController}/{mentorUserId}/requestMentorship/{studentUserId}");
            var response = await _client.GetAsync(uri);
            // return the mentorship request if request was successful
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var mentorshipRequest = JsonConvert.DeserializeObject<UserInfo>(json);
                return mentorshipRequest;
            }
            return new UserInfo();
        }

        public async Task<List<UserInfo>> GetNotificationsForMentor(string mentorUserId)
        {
            var uri = new Uri($"{ApiConstants.ApiUrl}{ApiConstants.StudentMentorController}/{mentorUserId}/mentorshipRequests");
            var response = await _client.GetAsync(uri);
            // return userRequestNotifications if request was successful
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var userRequestNotifications = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                return userRequestNotifications;
            }
            // return empty list if request was unsuccessful
            return new List<UserInfo>();
        }
    }
}