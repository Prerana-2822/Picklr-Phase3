using Microsoft.AspNetCore.Http;

namespace Picklr.Models
{
    public class PicklrSession
    {
        private const string CartKey = "cart";
        private const string ConfirmKey = "confirm";
        private const string ClubKey = "club";
        private const string DateKey = "date";
        private const string CountKey = "cartcount";
        private const string WishlistKey = "wishlist";


        private ISession Session { get; }


        public PicklrSession(ISession session)
        {
            Session = session;
        }



        // CART METHODS


        public void SetCartItems(
            List<CartItem> cartItems)
        {
            Session.SetObject(
                CartKey,
                cartItems
            );


            Session.SetInt32(
                CountKey,
                cartItems.Count
            );
        }



        public List<CartItem> GetCartItems()
        {
            return Session.GetObject<List<CartItem>>(
                CartKey
            ) ?? new List<CartItem>();
        }



        public int GetCartCount()
        {
            int? storedCount =
                Session.GetInt32(CountKey);


            if (storedCount.HasValue)
            {
                return storedCount.Value;
            }


            List<CartItem> cartItems =
                GetCartItems();


            int actualCount =
                cartItems.Count;


            Session.SetInt32(
                CountKey,
                actualCount
            );


            return actualCount;
        }



        public void RemoveCartItems()
        {
            Session.Remove(CartKey);


            Session.SetInt32(
                CountKey,
                0
            );
        }





        // CONFIRMATION METHODS


        public void SetConfirmItems(
            List<CartItem> confirmItems)
        {
            Session.SetObject(
                ConfirmKey,
                confirmItems
            );
        }



        public List<CartItem> GetConfirmItems()
        {
            return Session.GetObject<List<CartItem>>(
                ConfirmKey
            ) ?? new List<CartItem>();
        }



        public void RemoveConfirmItems()
        {
            Session.Remove(
                ConfirmKey
            );
        }





        // ACTIVE CLUB METHODS


        public void SetActiveClub(
            string activeClub)
        {
            Session.SetString(
                ClubKey,
                activeClub
            );
        }



        public string GetActiveClub()
        {
            return Session.GetString(
                ClubKey
            ) ?? "all";
        }





        // ACTIVE DATE METHODS


        public void SetActiveDate(
            DateTime activeDate)
        {
            Session.SetString(
                DateKey,
                activeDate.ToString("yyyy-MM-dd")
            );
        }



        public DateTime GetActiveDate()
        {
            string? storedDate =
                Session.GetString(DateKey);


            if (DateTime.TryParse(
                storedDate,
                out DateTime activeDate))
            {
                return activeDate.Date;
            }


            return DateTime.Today;
        }





        // WISHLIST METHODS


        public void SetWishlistItems(
            List<WishlistItem> wishlistItems)
        {
            Session.SetObject(
                WishlistKey,
                wishlistItems
            );
        }



        public List<WishlistItem> GetWishlistItems()
        {
            return Session.GetObject<List<WishlistItem>>(
                WishlistKey
            ) ?? new List<WishlistItem>();
        }



        public void RemoveWishlistItems()
        {
            Session.Remove(
                WishlistKey
            );
        }


    }
}