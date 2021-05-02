﻿using BookSharing.Domain.Base;

namespace BookSharing.Domain.UserBookAggregate
{
    public class UserBook : IAggregateRoot
    {
        public UserBook(int userId, long isbn, int rank)
        {
            UserId = userId;
            ISBN = isbn;
            Rank = rank;
        }
        private UserBook() { }

        public int Id { get; private set; }
        public int UserId { get; private set; }
        public long ISBN { get; private set; }
        public int Rank { get; private set; }
        public bool WasGiven { get; private set; }

        public void MarkAsGiven()
        {
            this.WasGiven = true;
        }
    }
}
