using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization; // Cần thiết cho việc Parse Float


namespace DoAnTinHoc_HK1_2025_2026
{
    public partial class Form2 : Form
    {

        public CustomerRecord NewRecord { get; private set; }
        private bool IsEditMode = false;
        private CustomerRecord _originalRecord;
        // Constructor mặc định: Dùng cho chế độ Thêm mới (Add)
        public Form2()
        {
            InitializeComponent();
            IsEditMode = false;
            InitializeControls();
        }
        public Form2(CustomerRecord recordToEdit)
        {
            InitializeComponent();
            IsEditMode = true;
            _originalRecord = recordToEdit; 
            InitializeControls(); 
            LoadRecordToForm(recordToEdit); 
        }
        private void InitializeControls()
        {
            // Khóa ID khi chỉnh sửa để tránh lỗi thay đổi key trong cây AVL
            txtID.Enabled = !IsEditMode;

            if (IsEditMode)
            {
                this.Text = "Chỉnh sửa Thông tin Khách hàng";
                btnThem.Text = "Cập nhật"; // Đổi tên nút
            }
            else
            {
                this.Text = "Thêm Khách hàng mới";
                btnThem.Text = "Thêm";
            }
        }
        private void LoadRecordToForm(CustomerRecord record)
        {
            if (record == null) return;

            txtID.Text = record.ID.ToString();
            txtAge.Text = record.Age.ToString();

            // Gender
            rdbMale.Checked = (record.Gender == CGender.Male);
            rdbFemale.Checked = (record.Gender == CGender.Female);

            txtItemPurchased.Text = record.ItemPurchased;
            txtCategory.Text = record.Category;
            txtPurchaseAmount.Text = record.PurchaseAmount.ToString();
            txtLocation.Text = record.Location;
            txtSize.Text = record.Size;
            txtColor.Text = record.Color;
            txtSeason.Text = record.Season;

            // Review Rating (sử dụng CultureInfo.InvariantCulture để hiển thị dấu chấm thập phân)
            txtReviewRating.Text = record.ReviewRating.ToString(CultureInfo.InvariantCulture);

            // Subscription Status
            rdbYesSubscriptionStatus.Checked = (record.SubscriptionStatus == CSubscriptionStatus.Yes);
            rdbNoSubscriptionStatus.Checked = (record.SubscriptionStatus == CSubscriptionStatus.No);

            txtShippingType.Text = record.ShippingType;

            // Discount Applied
            rdbYesDiscountApplied.Checked = (record.DiscountApplied == CDiscountApplied.Yes);
            rdbNoDiscountApplied.Checked = (record.DiscountApplied == CDiscountApplied.No);

            // Promo Code Used
            rdbYesPromoCodeUsed.Checked = (record.PromoCodeUsed == CPromoCodeUsed.Yes);
            rdbNoPromoCodeUsed.Checked = (record.PromoCodeUsed == CPromoCodeUsed.No);

            txtPreviousPurchases.Text = record.PreviousPurchases.ToString();
            txtPaymentMethod.Text = record.PaymentMethod;
            txtFrequencyOfPurchases.Text = record.FrequencyOfPurchases;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            NewRecord = null;

            try
            {
                // 1. Thu thập và xác thực dữ liệu
                int customerID = int.Parse(txtID.Text.Trim());
                int age = int.Parse(txtAge.Text.Trim());


                CGender gender = rdbMale.Checked ? CGender.Male : CGender.Female;

                string itemPurchased = txtItemPurchased.Text.Trim();
                string category = txtCategory.Text.Trim();
                int purchaseAmount = int.Parse(txtPurchaseAmount.Text.Trim());
                string location = txtLocation.Text.Trim();
                string size = txtSize.Text.Trim();
                string color = txtColor.Text.Trim();
                string season = txtSeason.Text.Trim();


                float reviewRating = float.Parse(txtReviewRating.Text.Trim(), CultureInfo.InvariantCulture);


                CSubscriptionStatus subscriptionStatus = rdbYesSubscriptionStatus.Checked ? CSubscriptionStatus.Yes : CSubscriptionStatus.No;


                string shippingType = txtShippingType.Text.Trim();


                CDiscountApplied discountApplied = rdbYesDiscountApplied.Checked ? CDiscountApplied.Yes : CDiscountApplied.No;


                CPromoCodeUsed promoCodeUsed = rdbYesPromoCodeUsed.Checked ? CPromoCodeUsed.Yes : CPromoCodeUsed.No;


                int previousPurchases = int.Parse(txtPreviousPurchases.Text.Trim());


                string paymentMethod = txtPaymentMethod.Text.Trim();


                string frequencyOfPurchases = txtFrequencyOfPurchases.Text.Trim();


                // 2. Tạo đối tượng CustomerRecord mới
                NewRecord = new CustomerRecord(
                    customerID, age, gender, itemPurchased, category,
                    purchaseAmount, location, size, color, season,
                    reviewRating, subscriptionStatus, shippingType,
                    discountApplied, promoCodeUsed, previousPurchases,
                    paymentMethod, frequencyOfPurchases
                );

                // 3. Đặt DialogResult để báo cho Form1 dữ liệu đã sẵn sàng
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi định dạng. Vui lòng kiểm tra các trường số (ID, Age, Purchase Amount, Previous Purchases, Review Rating).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewRecord = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewRecord = null;
            }
        }
    }
}