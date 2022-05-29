using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using RestSharp;
using RestSharp.Authenticators;

namespace upbit.UpbitAPI
{
    public class NoParam
    {
        private string mUpbitAccessKey;
        private string mUpbitSecretKey;

        private readonly DateTime mDtTimeSpanStamp;
        private const string mBaseURL = "https://api.upbit.com";

        public NoParam(string upbitAccresKey, string upbitSecretKey)
        {
            this.mUpbitAccessKey = upbitAccresKey;
            this.mUpbitSecretKey = upbitSecretKey;
            this.mDtTimeSpanStamp = new DateTime(1970, 01, 01);
        }

        public async Task<string> Get(string path, Method method)
        {
            StringBuilder tokenSB = JWTNoParam();
            string token = tokenSB.ToString();

            tokenSB.Clear();
            tokenSB = null;

            RestClient client = new RestClient(mBaseURL);
            RestRequest request = new RestRequest(path, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            token = null;

            Task<IRestResponse> responseTask = client.ExecuteAsync(request); // 요청을 서버측에 보내 응답을 받음
            IRestResponse response = await responseTask;

            if (response.IsSuccessful)
            {
                return response.Content;
            }

            else
            {
                return null;
            }
        }
        public async Task<string> GetTask(string path, Method method)
        {
            StringBuilder tokenSB = JWTNoParam();
            string token = tokenSB.ToString();

            tokenSB.Clear();
            tokenSB = null;

            RestClient client = new RestClient(mBaseURL);
            RestRequest request = new RestRequest(path, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            token = null;

            IRestResponse response = await client.ExecuteAsync(request); // 요청을 서버측에 보내 응답을 받음
            if (response.IsSuccessful)
            {
                return response.Content;
            }

            else
            {
                return null;
            }
        }
        public async void GetAsync(string path, Method method, string sbReturn)
        {
            StringBuilder tokenSB = JWTNoParam();
            string token = tokenSB.ToString();

            tokenSB.Clear();
            tokenSB = null;

            RestClient client = new RestClient(mBaseURL);
            RestRequest request = new RestRequest(path, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);
            token = null;


            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                sbReturn = response.Content;
            }
            else
            {
                sbReturn = null;
            }
        }

        public StringBuilder JWTNoParam()
        {
            TimeSpan diff = DateTime.Now - mDtTimeSpanStamp;
            long nonce = Convert.ToInt64(diff.TotalMilliseconds);
            JwtPayload payLoad = new JwtPayload
            {
            {"access_key", this.mUpbitAccessKey },
            {"nonce", nonce}
            };

            byte[] keyBytes = Encoding.Default.GetBytes(this.mUpbitSecretKey);
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            JwtHeader header = new JwtHeader(credentials);
            JwtSecurityToken secToken = new JwtSecurityToken(header, payLoad);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);

            StringBuilder returnStr = new StringBuilder();
            returnStr.Append("Bearer ");
            returnStr.Append(jwtToken);
            return returnStr;
        }

    }
}
