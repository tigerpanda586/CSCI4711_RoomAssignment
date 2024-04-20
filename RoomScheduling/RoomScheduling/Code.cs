using System;
using System.Globalization;

namespace RoomScheduling.Entity
{
    public class RequestList
    {
        public Request[] requests;
        
        public Request[] getReq()
        {
            return requests;
        }
    }

    public class RoomList 
    {
        public Room[] rooms;

        public Room[] getRooms()
        {
            return rooms;
        }
    }

    public class Account
    {
        private string _usn;
        private string _pwd;
        private string _role;

        Account(string usn, string pass, string role)
        {
            usn = _usn;
            pass = _pwd;
            role = _role;
        }

        public string getUsn()
        {
            return _usn;
        }

        public string getPass()
        {
            return _pwd;
        }


    }

    public class Request
    {
        private int _reqNo;
        private string _usn;
        private int _roomNo;
        private bool _status;
        private DateTime _date;
        private string _subject;
        public Request(int reqNo, string usn, int roomNo, bool status, DateTime date, string subject)
        {
            _reqNo = reqNo;
            _usn = usn;
            _roomNo = roomNo;
            _status = status;
            _date = date;
            _subject = subject;
        }

        public string getUsn()
        {
            return _usn;
        }

        public int getRoomNo()
        {
            return _roomNo;
        }

        public void setRoom(int roomNum)
        {
            _roomNo = roomNum;
        }

        public bool getStatus()
        {
            return _status;
        }

        public void setStatus(bool status)
        {
            _status = status;
        }

        public string getSubject()
        {
            return _subject;
        }

        public int getReqNo()
        {
            return _reqNo;
        }

    }

    public class Room
    {
        private int _roomNo;
        private DateTime _date;
        private int _capacity;
        private int _currOccupants;
        private string _subject;

        public Room(int roomNo, DateTime date, int capacity, int currOccupants, string subject)
        {
            _roomNo = roomNo;
            _date = date;
            _capacity = capacity;
            _currOccupants = currOccupants;
            _subject = subject;
        }

        public int getRoomNo()
        {
            return _roomNo;
        }

        public DateTime getDate()
        {
            return _date;
        }

        public int getCapacity()
        {
            return _capacity;
        }

        public int getCurrOccupants()
        {
            return _currOccupants;
        }

        public void setCurrOccupants(int currOccupants)
        {
            _currOccupants = currOccupants;
        }

        public string getSubject()
        {
            return _subject;
        }
    }

}
