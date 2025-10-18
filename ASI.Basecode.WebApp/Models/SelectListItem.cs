namespace ASI.Basecode.WebApp.Models
{
    /// 
    /// Select Dropdown List Model
    /// 
    public class SelectListItem
    {
        /// 
        /// SelectListItem default constructor
        /// 
        public SelectListItem()
        {
        }

        /// 
        /// Populates Select List Item 
        /// 
        /// <param name="text"></param>
        /// <param name="value"></param>
        public SelectListItem(string text = "", string value = "")
        {
            this.Label = text;
            this.Value = value;
        }

        /// 
        /// Select dropdown item label
        /// 
        public string Label { get; set; }
        /// 
        /// Select dropdown item value
        /// 
        public string Value { get; set; }

        public string BgColor { get; set; }
    }
}
