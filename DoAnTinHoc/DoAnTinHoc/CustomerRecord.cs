using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    // Định nghĩa các Enums
    public enum CGender { Male, Female }
    public enum CSubscriptionStatus { Yes, No }
    public enum CDiscountApplied { Yes, No }
    public enum CPromoCodeUsed { Yes, No }

    [Serializable]
    public class CustomerRecord
    {
        // Các thuộc tính
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
        public float ReviewRating { get; set; } // Kiểu float
        public CSubscriptionStatus SubscriptionStatus { get; set; }
        public string ShippingType { get; set; }
        public CDiscountApplied DiscountApplied { get; set; }   
        public CPromoCodeUsed PromoCodeUsed { get; set; }
        public int PreviousPurchases { get; set; }
        public string PaymentMethod { get; set; }
        public string FrequencyOfPurchases { get; set; }

        // Constructors
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

        public CustomerRecord() : this(0, 0, CGender.Female,
                                        "", "", 0, "", "",
                                        "", "", 0f, CSubscriptionStatus.No,
                                        "", CDiscountApplied.No, CPromoCodeUsed.No,
                                        0, "", "")
        {
        }

        // Phương thức Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        // PHƯƠNG THỨC ParseFromCsv (Đã sửa lỗi thiếu định nghĩa)
        public static CustomerRecord ParseFromCsv(string csvLine)
        {
            string[] fields = csvLine.Split(',');

            // Kiểm tra số lượng trường. Giả định có 18 trường.
            if (fields.Length != 18)
            {
                return null;
            }

            try
            {
                // Sử dụng object initializer để tạo đối tượng
                return new CustomerRecord
                {
                    ID = int.Parse(fields[0]),
                    Age = int.Parse(fields[1]),
                    // Chuyển đổi chuỗi thành Enum, sử dụng Enum.Parse với tham số 'true' để bỏ qua case
                    Gender = (CGender)Enum.Parse(typeof(CGender), fields[2].Trim(), true),
                    ItemPurchased = fields[3].Trim(),
                    Category = fields[4].Trim(),
                    PurchaseAmount = int.Parse(fields[5]),
                    Location = fields[6].Trim(),
                    Size = fields[7].Trim(),
                    Color = fields[8].Trim(),
                    Season = fields[9].Trim(),
                    ReviewRating = float.Parse(fields[10]), // Dùng float.Parse
                    SubscriptionStatus = (CSubscriptionStatus)Enum.Parse(typeof(CSubscriptionStatus), fields[11].Trim(), true),
                    ShippingType = fields[12].Trim(),
                    DiscountApplied = (CDiscountApplied)Enum.Parse(typeof(CDiscountApplied), fields[13].Trim(), true),
                    PromoCodeUsed = (CPromoCodeUsed)Enum.Parse(typeof(CPromoCodeUsed), fields[14].Trim(), true),
                    PreviousPurchases = int.Parse(fields[15]),
                    PaymentMethod = fields[16].Trim(),
                    FrequencyOfPurchases = fields[17].Trim()
                };
            }
            catch (Exception)
            {
                // Trả về null nếu có lỗi Format (ví dụ: chuỗi không hợp lệ cho số hoặc enum)
                return null;
            }
        }

        // PHƯƠNG THỨC ToString (Dùng để ghi file CSV)
        public override string ToString()
        {
            // Chuyển Enum thành chuỗi (ví dụ: CGender.Male -> "Male")
            return string.Join(",",
                ID, Age, Gender.ToString(), ItemPurchased, Category, PurchaseAmount,
                Location, Size, Color, Season, ReviewRating, SubscriptionStatus.ToString(),
                ShippingType, DiscountApplied.ToString(), PromoCodeUsed.ToString(), PreviousPurchases,
                PaymentMethod, FrequencyOfPurchases);
        }
    }
}