using System;

namespace MyAnimeGuide
{
    class AnimeDateTime
    {
        public DateTime StDateTime { get; set; }
        public DateTime EdDateTime { get; set; }
        public string AnimeDateTimeforView { get; set; }

        public AnimeDateTime(string StTime, string EdTime)
        {
            StDateTime = GetDateTimefromString(StTime);
            EdDateTime = GetDateTimefromString(EdTime);
            AnimeDateTimeforView = StDateTime.TimeOfDay.ToString() + "～" + EdDateTime.TimeOfDay.ToString();
        }

        /// <summary>
        /// "YYYYMMDDHHMM00"形式のstringをDatetime型にして返す
        /// </summary>
        /// <param name="strDateTime">"YYYYMMDDHHMM00"形式のstring</param>
        /// <returns></returns>
        private DateTime GetDateTimefromString(string strDateTime)
        {
            if (strDateTime.Length < 14)
            {
                Console.WriteLine("Error: " + strDateTime + " is not match for DateTime.");
                return DateTime.Now;
            }

            int year = int.Parse(strDateTime.Substring(0, 4));
            int month = int.Parse(strDateTime.Substring(4, 2));
            int day = int.Parse(strDateTime.Substring(6, 2));
            int hour = int.Parse(strDateTime.Substring(8, 2));
            int minutes = int.Parse(strDateTime.Substring(10, 2));

            return new DateTime(year,month,day,hour,minutes,0);

        }
    }
}
