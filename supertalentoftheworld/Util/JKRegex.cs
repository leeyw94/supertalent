using System.Text.RegularExpressions;

namespace WebSkill.Util
{
    public class JKRegex
    {
        public bool ChkHp(string hp)
        {
            string sPattern = "^\\d{3}-\\d{3}-\\d{4}$";

            if (Regex.IsMatch(hp, sPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //사이트 본디주소만 가져오는 메소드
        // 예를들어 http://localhost:6932/Home/bbs 라면 localhost:6932 만 가져온다.
        public string GetHostUrl(string fullUrl)
        {
            string result = "";
            var reg = new Regex(@"//(.*?)/");
            Match mat;
            mat = reg.Match(fullUrl);
            result = mat.Value;
            if (string.IsNullOrEmpty(result))
            {
            }
            else
            {
                result = result.Replace("/", string.Empty);
            }

            return result;
        }
    }
}