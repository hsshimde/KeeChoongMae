using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using RestSharp;


namespace upbit.UpbitAPI
{
    class Param
    {
        private string mUpbitAccesKey;
        private string mUpbitSecretKey;

        private readonly DateTime mDtTimeSpanStamp;
        private const string mBaseURL = "https://api.upbit.com";

        public Param(string upbitAccesKey, string upbitSecretKey)
        {
            this.mUpbitAccesKey = upbitAccesKey;
            this.mUpbitSecretKey = upbitSecretKey;
            this.mDtTimeSpanStamp = new DateTime(1970, 01, 01);
        }

        public async Task<string> Get(string path, Dictionary<string, string> parameters, Method method)
        {

            StringBuilder queryStringSb = GetQueryString(parameters);
            StringBuilder tokenSb = JWT_param(queryStringSb.ToString()); // 입력받은 변수를 JWT토큰으로 변환
            string token = tokenSb.ToString();

            queryStringSb.Insert(0, "?");      // 링크에 ?를 붙임으로 파라미터를 사용한다는 의미
            queryStringSb.Insert(0, path);

            // 여기까지오면 queryString는
            // '/path?key1=value1&key2=value2 ....' 이러한 형태가 된다. 이것을 RestRequest에 넣어주면 된다.

            var client = new RestClient(mBaseURL);       // RestSharp 클라이언트 생성
            var request = new RestRequest(queryStringSb.ToString(), method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);

            queryStringSb.Clear();
            queryStringSb = null;
            tokenSb.Clear();
            tokenSb = null;
            parameters.Clear();
            parameters = null;

            Task<IRestResponse> responseTask = client.ExecuteAsync(request);
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
        public StringBuilder GetQueryString(Dictionary<string, string> parameters)
        {
            // Dictionary 형태로 받은 key = value 형태를 
            // ?key1=value1&key2=value2 ... 형태로 만들어줌
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                builder.Append(pair.Key);
                builder.Append("=");
                builder.Append(pair.Value);
                builder.Append("&");
            }

            if (builder.Length > 0)
            {
                builder.Length = builder.Length - 1; // 마지막 &를 제거하기 위함.
            }
            return builder;
        }

        public StringBuilder JWT_param(string queryString)
        {

            SHA512 sha512 = SHA512.Create();
            byte[] queryHashByteArray = sha512.ComputeHash(Encoding.UTF8.GetBytes(queryString));
            string queryHash = BitConverter.ToString(queryHashByteArray).Replace("-", "").ToLower();

            TimeSpan diff = DateTime.Now - mDtTimeSpanStamp;
            var nonce = Convert.ToInt64(diff.TotalMilliseconds);

            var payload = new JwtPayload
                    {
                        { "access_key", this.mUpbitAccesKey },
                        { "nonce", nonce  },
                        { "query_hash", queryHash },
                        { "query_hash_alg", "SHA512" }
                    };

            byte[] keyBytes = Encoding.Default.GetBytes(this.mUpbitSecretKey);
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);

            StringBuilder returnStr = new StringBuilder();
            returnStr.Append("Bearer "); // 띄어쓰기 한칸 있어야함 주의!
            returnStr.Append(jwtToken);

            return returnStr;
        }
    }
}
