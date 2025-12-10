using System;
using System.Windows.Forms;
using System.Globalization;

namespace DoAnTinHoc
{

    public partial class QLData : Form
    {

        public CustomerRecord NewCustomerRecord { get; private set; }

        public QLData()
        {
            InitializeComponent();
            ClearInputs();
            btnThem.Text = "Thêm mới"; // Đổi lại text cho rõ ràng hơn trong chế độ Thêm
            this.Text = "NHẬP LIỆU KHÁCH HÀNG MỚI";
        }
        public QLData(CustomerRecord recordToEdit) : this()
        {
            if (recordToEdit != null)
            {
                // Tải dữ liệu vào các controls
                LoadRecordToInputs(recordToEdit);

                // Khóa trường ID để không cho phép thay đổi khóa chính khi Sửa
                txtID.ReadOnly = true;

                btnThem.Text = "Cập Nhật"; // Đổi text cho rõ ràng trong chế độ Sửa
                this.Text = "CẬP NHẬT THÔNG TIN KHÁCH HÀNG";
            }
        }
        private void LoadRecordToInputs(CustomerRecord record)
        {
            txtID.Text = record.ID.ToString();
            txtAge.Text = record.Age.ToString();
            txtItemPurchased.Text = record.ItemPurchased;
            txtCategory.Text = record.Category;
            // Dùng InvariantCulture để đảm bảo dấu thập phân là dấu chấm
            txtPurchaseAmount.Text = record.PurchaseAmount.ToString();
            txtLocation.Text = record.Location;
            txtSize.Text = record.Size;
            txtColor.Text = record.Color;
            txtSeason.Text = record.Season;
            txtReviewRating.Text = record.ReviewRating.ToString(CultureInfo.InvariantCulture);
            txtShippingType.Text = record.ShippingType;
            txtPreviousPurchases.Text = record.PreviousPurchases.ToString();
            txtPaymentMethod.Text = record.PaymentMethod;
            txtFrequencyOfPurchases.Text = record.FrequencyOfPurchases;

            // Radio Buttons
            rdbMale.Checked = (record.Gender == CGender.Male);
            rdbFemale.Checked = (record.Gender == CGender.Female);
            rdbYesSubscriptionStatus.Checked = (record.SubscriptionStatus == CSubscriptionStatus.Yes);
            rdbNoSubscriptionStatus.Checked = (record.SubscriptionStatus == CSubscriptionStatus.No);
            rdbYesDiscountApplied.Checked = (record.DiscountApplied == CDiscountApplied.Yes);
            rdbNoDiscountApplied.Checked = (record.DiscountApplied == CDiscountApplied.No);
            rdbYesPromoCodeUsed.Checked = (record.PromoCodeUsed == CPromoCodeUsed.Yes);
            rdbNoPromoCodeUsed.Checked = (record.PromoCodeUsed == CPromoCodeUsed.No);
        }
        private CustomerRecord CreateRecordFromInputs()
        {
            // --- 1. Đọc và Kiểm tra ID (Khóa chính) ---
            if (!int.TryParse(txtID.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Mã KH phải là số nguyên dương hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // --- 2. Đọc các trường số học ---
            if (!int.TryParse(txtAge.Text, out int age)) { age = 0; }
            if (!int.TryParse(txtPurchaseAmount.Text, out int purchaseAmount)) { purchaseAmount = 0; }
            if (!int.TryParse(txtPreviousPurchases.Text, out int prevPurchases)) { prevPurchases = 0; }

            // Dùng InvariantCulture để đảm bảo parsing dấu thập phân (dấu chấm) không bị lỗi
            if (!float.TryParse(txtReviewRating.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out float reviewRating)) { reviewRating = 0.0f; }

            // --- 3. Đọc các trường Enum (Radio Buttons) ---
            CGender gender = rdbFemale.Checked ? CGender.Female : CGender.Male;
            CSubscriptionStatus subStatus = rdbYesSubscriptionStatus.Checked ? CSubscriptionStatus.Yes : CSubscriptionStatus.No;
            CDiscountApplied discount = rdbYesDiscountApplied.Checked ? CDiscountApplied.Yes : CDiscountApplied.No;
            CPromoCodeUsed promo = rdbYesPromoCodeUsed.Checked ? CPromoCodeUsed.Yes : CPromoCodeUsed.No;

            // --- 4. Đọc các trường String ---
            string itemPurchased = txtItemPurchased.Text;
            string category = txtCategory.Text;
            string location = txtLocation.Text;
            string size = txtSize.Text;
            string color = txtColor.Text;
            string season = txtSeason.Text;
            string shippingType = txtShippingType.Text;
            string paymentMethod = txtPaymentMethod.Text;
            string frequencyOfPurchases = txtFrequencyOfPurchases.Text;

            CustomerRecord record = new CustomerRecord(
              customerID: id,
              age: age,
              gender: gender,
              itemPurchased: itemPurchased,
              category: category,
              purchaseAmount: purchaseAmount,
              location: location,
              size: size,
              color: color,
              season: season,
              reviewRating: reviewRating,
              subscriptionStatus: subStatus,
              shippingType: shippingType,
              discountApplied: discount,
              promoCodeUsed: promo,
              previousPurchases: prevPurchases,
              paymentMethod: paymentMethod,
              frequencyOfPurchases: frequencyOfPurchases
            );

            return record;
        }

        // Hàm hỗ trợ: Xóa trắng các trường nhập liệu
        private void ClearInputs()
        {
            txtID.Text = "";
            txtAge.Text = "20";
            txtItemPurchased.Text = "Blouse";
            txtCategory.Text = "Clothing";
            txtPurchaseAmount.Text = "100";
            txtLocation.Text = "Oregon";
            txtSize.Text = "S";
            txtColor.Text = "Gray";
            txtSeason.Text = "Summer";
            txtReviewRating.Text = "4.0";
            txtShippingType.Text = "Standard";
            txtPreviousPurchases.Text = "1";
            txtPaymentMethod.Text = "Cash";
            txtFrequencyOfPurchases.Text = "Monthly";

            rdbMale.Checked = true;
            rdbNoSubscriptionStatus.Checked = true;
            rdbNoDiscountApplied.Checked = true;
            rdbNoPromoCodeUsed.Checked = true;
        }

        // Logic cho nút THÊM/CẬP NHẬT (btnThem)
        private void btnThem_Click(object sender, EventArgs e)
        {
            CustomerRecord newRecord = CreateRecordFromInputs();
            if (newRecord == null) return;

            // Gán bản ghi vào public property để form QLAVL có thể lấy
            NewCustomerRecord = newRecord;

            // Đặt DialogResult là OK và đóng form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Logic cho nút THOÁT (btnThoat)
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Đặt DialogResult là Cancel và đóng form
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}