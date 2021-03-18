using System;

namespace WeTravel.Domain
{
    public class Session
    {
        public Guid Token { get; set; }
        public User User { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Session session)
            {
                result = this.Token.Equals(session.Token);
            }

            return result;
        }

    }
}
