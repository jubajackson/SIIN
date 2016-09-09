using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace SecondIINoneMC.Core.Security
{
    public class Principal :
                            IPrincipal,
                            ISerializable
    {

        #region Constants

        #region Private Constants

        #region Serialization Fields
        private const String _fieldIdentity = "Identity";
        private const String _fieldUserId = "UserId";
        #endregion Serialization Fields

        #endregion

        #endregion

        #region Member Variables

        #region Private Members
        private IIdentity _identity;

        private Int32 _userId = Int32.MaxValue;
        private Int32 _timeZoneId = 0;
        #endregion

        #endregion

        #region Constructors

        #region Internal Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        protected internal Principal()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        protected internal Principal(IIdentity identity)
        {
            _identity = identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        protected internal Principal(Int32 userId)
        {
            _userId = userId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="userId"></param>
        protected internal Principal(IIdentity identity, Int32 userId)
        {
            _identity = identity;
            _userId = userId;
        }

        #endregion

        #region Private Constructors

        protected Principal(SerializationInfo info, StreamingContext context)
        {
            _identity = (IIdentity)info.GetValue(_fieldIdentity, typeof(IIdentity));
            _userId = info.GetInt32(_fieldUserId);
        }
        #endregion

        #endregion

        #region ISerializable Implementation

        /// <summary>
        /// ISerializable's GetObjectData Implementation that serializes the Object.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(_fieldIdentity, _identity, typeof(IIdentity));
            info.AddValue(_fieldUserId, _userId);
        }

        #endregion

        #region Properties

        #region Protected Properties

        /// <summary>
        /// <para>Returns the identity.</para>
        /// </summary>
        /// <value></value>
        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                return _identity;
            }
        }

        /// <summary>
        /// <para>User Id</para>
        /// </summary>
        /// <value></value>
        public Int32 UserId
        {
            get
            {
                return _userId;
            }
        }

        /// <summary>
        /// Time Zone for Localization
        /// </summary>
        public Int32 TimeZoneId
        {
            get
            {
                return _timeZoneId;
            }
            set
            {
                _timeZoneId = value;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Public Methods
        public bool IsInRole(string roleName)
        {
            return true;
        }

        #endregion

        #region Static Public Methods

        /// <summary>
        /// Returns the current thread's context as a <see cref="SecondIINoneMC.Core.Principal" />.
        /// </summary>
        public static Principal Current
        {
            get
            {
                return System.Threading.Thread.CurrentPrincipal as Principal;
            }
        }
        #endregion

        #endregion

    }
}
