using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoomScheduling
{
    public class LoginConnector
    {
    /* Whitelist
     *  Usr
     *      Email should end with @university.edu 
     *      Email address should be 3 - 15 characters long (before @) 
     *      Email address can only contain letters  
     *  Pwd
     *      Can be 8 – 20 characters  
     *      Can contain certain characters: !@#$%
     *      Can contain letters and numbers
     * 
     * Blacklist
     *  Usr
     *      
     *  Pwd
     *      Cannot contain characters except those specified in whitelist
     */

        public bool validateInput(string usr, string pwd) // returns true if valid
        {
            string emailEnd;
            string emailStart;

            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd) || !usr.Contains('@')) // Usr and pwd should not be empty/null. Usr must contain @ somewhere
                return false;

            usr = usr.Trim().ToLower(); //Can trim now that they are not null
            pwd = pwd.Trim();
            string[] str = usr.Split('@'); // splitting the email at @
            emailStart = str[0];
            emailEnd = str[1];


            // Checking username
            if (!usr.EndsWith("@university.edu") || emailEnd != "university.edu") // Email should end with @university.edu. this also covers if the user types @@. probably unnecessary
                return false;

            if (usr.Length > 30 || usr.Length < 18) // Email length should be 18 - 30 long
                return false;

            foreach (char c in emailStart) // 1st half of email should be only letters
            {
                if ((int)c < 97 || (int)c > 122) // checking ASCII values
                    return false;
            }


            //Checking password
            char[] blacklist = { '=', '*', '\\', ' ', ',', '.', ';', '(', ')', '\'', '\"' }; // Some significant characters in sql

            // Regex pattern. This requires the password to have at least 1 uppercase, 1 lower case, 1 number, 1 special character, no other characters, and 8 - 20 long
            // This is the same regex pattern from the Ppt slides, except for the added restriction:  (?!.*[^A-Za-z0-9!@#$])
            string pattern = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$])(?!.*[^A-Za-z0-9!@#$]).{8,20}$";

            foreach (char b in blacklist) // pwd cannot contain blacklist. This is likely unecessary because the regex pattern blocks all characters except letters, numbers, and !@#$
            {
                if (pwd.Contains(b))
                    return false;
            }

            if (!Regex.IsMatch(pwd, pattern)) // If the required pattern is not found, return false
                return false;


            return true; // return true if no problems were found above
        }
    }
}
