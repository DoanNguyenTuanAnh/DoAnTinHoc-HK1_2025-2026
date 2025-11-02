    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DoAnTinHoc_HK1_2025_2026
    {
        public enum CGender { Male, Female }

        [Serializable]
        internal class CustomerRecord
        {
            public int CustomerID { get; set; }
            public int Age { get; set; }
            public CGender Gender { get; set; }
            public string ItemPurchased { get; set; }
            public string Category { get; set; }
            public int PurchaseAmount { get; set; }
            public string Location { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public string Season { get; set; }
            public float ReviewRating { get; set; }
            public string SubscriptionStatus { get; set; }
            public string ShippingType { get; set; }
            public string DiscountApplied { get; set; }
            public string PromoCodeUsed { get; set; }
            public int PreviousPurchases { get; set; }
            public string PaymentMethod { get; set; }
            public string FrequencyOfPurchases { get; set; }
            public CustomerRecord(int customerID, int age, CGender gender,
                string itemPurchased, string category, int purchaseAmount,
                string location, string size, string color, string season,
                float reviewRating, string subscriptionStatus, string shippingType,
                string discountApplied, string promoCodeUsed, int previousPurchases,
                string paymentMethod, string frequencyOfPurchases)
            {
                CustomerID = customerID;
                Age = age;
                Gender = gender;
                ItemPurchased = itemPurchased;
                Category = category;
                PurchaseAmount = purchaseAmount;
                Location = location;
                Size = size;
                Color = color;
                Season = season;
                ReviewRating = reviewRating;
                SubscriptionStatus = subscriptionStatus;
                ShippingType = shippingType;
                DiscountApplied = discountApplied;
                PromoCodeUsed = promoCodeUsed;
                PreviousPurchases = previousPurchases;
                PaymentMethod = paymentMethod;
                FrequencyOfPurchases = frequencyOfPurchases;
            }

            public CustomerRecord() : this(0, 0, CGender.Female, "", "", 0, "", "", "", "", 0, "", "", "", "", 0, "", "")
            {
            }
            
        }
    }
