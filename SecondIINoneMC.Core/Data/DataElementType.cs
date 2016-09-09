using System.Data;

namespace Multiview.Data
{
    /// <summary>
    /// Specifies known database types of a field.
    /// </summary>
    /// <remarks>
    /// This enumeration was created to allow the encapsilation of the 
    /// database type needed while added parameters to the BaseDACommand
    /// class.
    /// </remarks>
    public enum DataElementType
    {
        /// <summary>
        /// BigInt
        /// </summary>
        BigInt = SqlDbType.BigInt,
        /// <summary>
        /// Binary
        /// </summary>
        Binary = SqlDbType.Binary,
        /// <summary>
        /// Bit
        /// </summary>
        Bit = SqlDbType.Bit,
        /// <summary>
        /// Char
        /// </summary>
        Char = SqlDbType.Char,
        /// <summary>
        /// Date
        /// </summary>
        Date = SqlDbType.Date,
        /// <summary>
        /// DateTime
        /// </summary>
        DateTime = SqlDbType.DateTime,
        /// <summary>
        /// Decimal
        /// </summary>
        Decimal = SqlDbType.Decimal,
        /// <summary>
        /// Float
        /// </summary>
        Float = SqlDbType.Float,
        /// <summary>
        /// UniqueIdentifier
        /// </summary>
        Guid = SqlDbType.UniqueIdentifier,
        /// <summary>
        /// Image
        /// </summary>
        Image = SqlDbType.Image,
        /// <summary>
        /// Integer
        /// </summary>
        Int = SqlDbType.Int,
        /// <summary>
        /// Money
        /// </summary>
        Money = SqlDbType.Money,
        /// <summary>
        /// Char
        /// </summary>
        NChar = SqlDbType.NChar,
        /// <summary>
        /// Text
        /// </summary>
        NText = SqlDbType.NText,
        /// <summary>
        /// VarChar
        /// </summary>
        NVarChar = SqlDbType.NVarChar,
        /// <summary>
        /// Real
        /// </summary>
        Real = SqlDbType.Real,
        /// <summary>
        /// SmallDateTime
        /// </summary>
        SmallDateTime = SqlDbType.SmallDateTime,
        /// <summary>
        /// SmallInt
        /// </summary>
        SmallInt = SqlDbType.SmallInt,
        /// <summary>
        /// SmallMoney
        /// </summary>
        SmallMoney = SqlDbType.SmallMoney,
        /// <summary>
        /// Text
        /// </summary>
        Text = SqlDbType.Text,
        /// <summary>
        /// Time
        /// </summary>
        Time = SqlDbType.Time,
        /// <summary>
        /// TimeStamp
        /// </summary>
        Timestamp = SqlDbType.Timestamp,
        /// <summary>
        /// TinyInt
        /// </summary>
        TinyInt = SqlDbType.TinyInt,
        /// <summary>
        /// UniqueIdentifier
        /// </summary>
        Udt = SqlDbType.UniqueIdentifier,
        /// <summary>
        /// UniqueIdentifier
        /// </summary>
        UniqueIdentifier = SqlDbType.UniqueIdentifier,
        /// <summary>
        /// VarBinary
        /// </summary>
        VarBinary = SqlDbType.VarBinary,
        /// <summary>
        /// VarChar
        /// </summary>
        VarChar = SqlDbType.VarChar,
        /// <summary>
        /// VarChar
        /// </summary>
        Variant = SqlDbType.VarChar,
        /// <summary>
        /// VarCha
        /// </summary>
        Xml = SqlDbType.NVarChar
    }
}
