using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debug = System.Diagnostics.Debug;

namespace SecondIINoneMC.Attributes
{
    /// <summary>
    /// Author Attribute captures the name of the author. Must be implemented in every Jitu source module.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Enum |
                    AttributeTargets.Struct, Inherited = true,
                    AllowMultiple = true)]
    [Author(@"Juba Jackson", 1.0)]
    public sealed class AuthorAttribute : System.Attribute
    {

        #region Member Variables

        #region Private Members
        private String _name = String.Empty;
        private Double _version = 1.0;
        #endregion

        #endregion

        #region Constructors

        #region Public Constructors

        /// <summary>
        /// AuthorAttribute constructor that takes in the name of the Author that wrote the class / struct / interface.
        /// </summary>
        /// <param name="name">Full Name of the Author</param>
        /// <param name="version">Version number of the code</param>
        /// <example>
        ///		<code>
        ///			using Jitu.Utilities.Attributes;
        /// 
        /// 
        ///			[Author("Jitu User 1", 1.0)]
        ///			[Author("Jitu User 2", 1.0)]
        /// 
        ///			public abstract class BaseEntity
        ///			{
        ///			}
        ///		</code>
        /// </example>
        /// <remarks>None</remarks>
        public AuthorAttribute(String name, Double version)
        {
            _name = name;
            _version = version;
        }

        #endregion

        #endregion

        #region Properties

        #region Public Properties

        /// <summary>
        /// Read Only Property that returns the name of the author
        /// </summary>
        /// <value>Name of the author</value>
        /// <remarks>None</remarks>
        public String Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Read Only Property that returns the Major and Minor version of the code
        /// </summary>
        /// <value>Version of the code</value>
        /// <remarks>None</remarks>
        public Double Version
        {
            get
            {
                return _version;
            }
        }

        #endregion

        #endregion

    }
}
