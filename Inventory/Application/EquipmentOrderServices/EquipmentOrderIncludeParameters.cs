namespace Inventory.Application.EquipmentOrderServices
{
    public struct EquipmentOrderIncludeParameters
    {
        public bool isIncludeEquipment;
        public bool isIncludeSerialNumber;
        public bool isIncludeAuthor;
        public bool isIncludeAssignee;
        public bool isIncludeLocation;

        //public EquipmentOrderIncludeParameters()
        //{
        //    isIncludeEquipment = false;
        //    isIncludeSerialNumber = false;
        //    isIncludeAuthor = false;
        //    isIncludeAssignee = false;
        //    isIncludeLocation = false;
        //}
    }
}