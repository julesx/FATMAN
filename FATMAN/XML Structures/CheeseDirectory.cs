using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace FATMAN.XML_Structures
{
    [XmlRoot(ElementName = "Cheese")]
    public class Cheese
    {
        public string Manufacturer
        {
            get
            {
                if (ManufacturerNameEn == null && ManufacturerNameFr == null)
                    return null;

                return ManufacturerNameFr == null ? ManufacturerNameEn : ManufacturerNameFr;
            }
        }

        public string CheeseName
        {
            get
            {
                return CheeseNameEn == null ? CheeseNameFr : CheeseNameEn;
            }
        }

        public string Characteristics
        {
            get
            {
                if (CharacteristicsFr == null && CharacteristicsEn == null)
                    return null;

                return CharacteristicsEn == null ? CharacteristicsFr.Trim('.') : CharacteristicsEn.Trim('.');
            }
        }

        public string Flavour
        {
            get
            {
                if (FlavourEn == null && FlavourFr == null)
                    return null;

                return FlavourEn == null ? FlavourFr.Trim('.') : FlavourEn.Trim('.');
            }
        }

        public string CategoryType
        {
            get
            {
                if (CategoryTypeFr == null && CategoryTypeEn == null)
                    return null;

                return CategoryTypeEn == null ? CategoryTypeFr.Trim('.') : CategoryTypeEn.Trim(',');
            }
        }

        [XmlElement(ElementName = "CheeseId")]
        public string CheeseId { get; set; }
        [XmlElement(ElementName = "CheeseNameFr")]
        public string CheeseNameFr { get; set; }
        [XmlElement(ElementName = "CheeseNameEn")]
        public string CheeseNameEn { get; set; }
        [XmlElement(ElementName = "ManufacturerNameFr")]
        public string ManufacturerNameFr { get; set; }
        [XmlElement(ElementName = "ManufacturerNameEn")]
        public string ManufacturerNameEn { get; set; }
        [XmlElement(ElementName = "ManufacturerProvCode")]
        public string ManufacturerProvCode { get; set; }
        [XmlElement(ElementName = "ManufacturingTypeEn")]
        public string ManufacturingTypeEn { get; set; }
        [XmlElement(ElementName = "ManufacturingTypeFr")]
        public string ManufacturingTypeFr { get; set; }
        [XmlElement(ElementName = "FatContentPercent")]
        public string FatContentPercent { get; set; }
        [XmlElement(ElementName = "MoisturePercent")]
        public string MoisturePercent { get; set; }
        [XmlElement(ElementName = "FlavourEn")]
        public string FlavourEn { get; set; }
        [XmlElement(ElementName = "FlavourFr")]
        public string FlavourFr { get; set; }
        [XmlElement(ElementName = "CharacteristicsEn")]
        public string CharacteristicsEn { get; set; }
        [XmlElement(ElementName = "CharacteristicsFr")]
        public string CharacteristicsFr { get; set; }
        [XmlElement(ElementName = "RipeningEn")]
        public string RipeningEn { get; set; }
        [XmlElement(ElementName = "RipeningFr")]
        public string RipeningFr { get; set; }
        [XmlElement(ElementName = "Organic")]
        public string Organic { get; set; }
        [XmlElement(ElementName = "CategoryTypeEn")]
        public string CategoryTypeEn { get; set; }
        [XmlElement(ElementName = "CategoryTypeFr")]
        public string CategoryTypeFr { get; set; }
        [XmlElement(ElementName = "MilkTypeEn")]
        public string MilkTypeEn { get; set; }
        [XmlElement(ElementName = "MilkTypeFr")]
        public string MilkTypeFr { get; set; }
        [XmlElement(ElementName = "MilkTreatmentTypeEn")]
        public string MilkTreatmentTypeEn { get; set; }
        [XmlElement(ElementName = "MilkTreatmentTypeFr")]
        public string MilkTreatmentTypeFr { get; set; }
        [XmlElement(ElementName = "RindTypeEn")]
        public string RindTypeEn { get; set; }
        [XmlElement(ElementName = "RindTypeFr")]
        public string RindTypeFr { get; set; }
        [XmlElement(ElementName = "LastUpdateDate")]
        public string LastUpdateDate { get; set; }
    }

    [XmlRoot(ElementName = "CheeseDirectory")]
    public class CheeseDirectory
    {
        [XmlElement(ElementName = "Cheese")]
        public List<Cheese> Cheese { get; set; }
    }
}
