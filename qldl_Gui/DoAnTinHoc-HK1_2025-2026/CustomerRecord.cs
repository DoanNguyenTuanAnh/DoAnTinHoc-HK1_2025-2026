    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using System.Globalization;    

    namespace DoAnTinHoc_HK1_2025_2026
    {
        public enum CGender { Male, Female }
        public enum CSubscriptionStatus { Yes, No }
        public enum CDiscountApplied { Yes, No }
        public enum CPromoCodeUsed {  Yes, No }

        [Serializable]
        public class CustomerRecord
        {
            public int ID { get; set; }
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
            public CSubscriptionStatus SubscriptionStatus { get; set; }
            public string ShippingType { get; set; }
            public CDiscountApplied DiscountApplied { get; set; }
            public CPromoCodeUsed PromoCodeUsed { get; set; }
            public int PreviousPurchases { get; set; }
            public string PaymentMethod { get; set; }
            public string FrequencyOfPurchases { get; set; }
            public CustomerRecord(int customerID, int age, CGender gender,
                string itemPurchased, string category, int purchaseAmount,
                string location, string size, string color, string season,
                float reviewRating, CSubscriptionStatus subscriptionStatus, string shippingType,
                CDiscountApplied discountApplied, CPromoCodeUsed promoCodeUsed, int previousPurchases,
                string paymentMethod, string frequencyOfPurchases)
            {
                ID = customerID;
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

            public CustomerRecord() : this(0, 0, CGender.Female, "", "", 0, "", "", "", "", 0, CSubscriptionStatus.Yes, "", CDiscountApplied.Yes, CPromoCodeUsed.Yes, 0, "", "")
            {
            }

       
        public string ToCsvString()
        {
            
            return $"{ID}," +
                   $"{Age}," +
                   $"{Gender.ToString().ToLower()}," + // Male/Female
                   $"{ItemPurchased}," +
                   $"{Category}," +
                   $"{PurchaseAmount}," +
                   $"{Location}," +
                   $"{Size}," +
                   $"{Color}," +
                   $"{Season}," +
                   $"{ReviewRating.ToString(CultureInfo.InvariantCulture)}," + // Dấu chấm cho số thập phân
                   $"{SubscriptionStatus.ToString().ToLower()}," + // Yes/No
                   $"{ShippingType}," +
                   $"{DiscountApplied.ToString().ToLower()}," + // Yes/No
                   $"{PromoCodeUsed.ToString().ToLower()}," + // Yes/No
                   $"{PreviousPurchases}," +
                   $"{PaymentMethod}," +
                   $"{FrequencyOfPurchases}";
        }
    }
    }
