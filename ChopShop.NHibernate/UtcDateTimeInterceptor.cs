using System;
using System.Collections;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace ChopShop.NHibernate
{
    /// <summary>
    /// http://www.milkcarton.com/blog/CategoryView,category,NHibernate.aspx
    /// </summary>
    public class UtcDateTimeInterceptor : IInterceptor
    {
        /// <summary>
        /// Called just before an object is initialized
        /// </summary>
        /// <param name="entity"/><param name="id"/><param name="propertyNames"/><param name="state"/><param name="types"/>
        /// <remarks>
        /// The interceptor may change the <c>state</c>, which will be propagated to the persistent
        ///             object. Note that when this method is called, <c>entity</c> will be an empty
        ///             uninitialized instance of the class.
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> if the user modified the <c>state</c> in any way
        /// </returns>
        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            ConvertDatabaseDateTimeToUtc(state, types);
            return true;
        }

        private void ConvertDatabaseDateTimeToUtc(object[] state, IType[] types)
        {
            int index = 0;
            foreach (var type in types)
            {
                if ((type.ReturnedClass == typeof(DateTime)) && state[index] != null && (((DateTime)state[index]).Kind == DateTimeKind.Unspecified))
                {
                    DateTime current = (DateTime)state[index];
                    DateTime result = DateTime.SpecifyKind(current, DateTimeKind.Local);
                    state[index] = result;
                }
                index++;
            }
        }

        /// <summary>
        /// Called when an object is detected to be dirty, during a flush.
        /// </summary>
        /// <param name="currentState"/><param name="entity"/><param name="id"/><param name="previousState"/><param name="propertyNames"/><param name="types"/>
        /// <remarks>
        /// The interceptor may modify the detected <c>currentState</c>, which will be propagated to
        ///             both the database and the persistent object. Note that all flushes end in an actual
        ///             synchronization with the database, in which as the new <c>currentState</c> will be propagated
        ///             to the object, but not necessarily (immediately) to the database. It is strongly recommended
        ///             that the interceptor <b>not</b> modify the <c>previousState</c>.
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> if the user modified the <c>currentState</c> in any way
        /// </returns>
        public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            ConvertLocalDateToUtc(currentState, types);
            return true;
        }

        /// <summary>
        /// Called before an object is saved
        /// </summary>
        /// <param name="entity"/><param name="id"/><param name="propertyNames"/><param name="state"/><param name="types"/>
        /// <remarks>
        /// The interceptor may modify the <c>state</c>, which will be used for the SQL <c>INSERT</c>
        ///             and propagated to the persistent object
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> if the user modified the <c>state</c> in any way
        /// </returns>
        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            ConvertLocalDateToUtc(state, types);
            return true;
        }

        private void ConvertLocalDateToUtc(object[] state, IType[] types)
        {
            int index = 0;
            foreach (var type in types)
            {
                if ((type.ReturnedClass == typeof(DateTime)) && state[index] != null && (((DateTime)state[index]).Kind == DateTimeKind.Utc))
                {
                    state[index] = ((DateTime)state[index]).ToUniversalTime();
                }
                index++;
            }
        }

        /// <summary>
        /// Called before an object is deleted
        /// </summary>
        /// <param name="entity"/><param name="id"/><param name="propertyNames"/><param name="state"/><param name="types"/>
        /// <remarks>
        /// It is not recommended that the interceptor modify the <c>state</c>.
        /// </remarks>
        public void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called before a collection is (re)created.
        /// </summary>
        public void OnCollectionRecreate(object collection, object key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called before a collection is deleted.
        /// </summary>
        public void OnCollectionRemove(object collection, object key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called before a collection is updated.
        /// </summary>
        public void OnCollectionUpdate(object collection, object key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called before a flush
        /// </summary>
        /// <param name="entities">The entities</param>
        public void PreFlush(ICollection entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called after a flush that actually ends in execution of the SQL statements required to
        ///             synchronize in-memory state with the database.
        /// </summary>
        /// <param name="entities">The entitites</param>
        public void PostFlush(ICollection entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when a transient entity is passed to <c>SaveOrUpdate</c>.
        /// </summary>
        /// <remarks>
        /// The return value determines if the object is saved
        /// <list>
        /// <item>
        /// <see langword="true"/> - the entity is passed to <c>Save()</c>, resulting in an <c>INSERT</c>
        /// </item>
        /// <item>
        /// <see langword="false"/> - the entity is passed to <c>Update()</c>, resulting in an <c>UPDATE</c>
        /// </item>
        /// <item>
        /// <see langword="null"/> - Hibernate uses the <c>unsaved-value</c> mapping to determine if the object is unsaved
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="entity">A transient entity</param>
        /// <returns>
        /// Boolean or <see langword="null"/> to choose default behaviour
        /// </returns>
        public bool? IsTransient(object entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called from <c>Flush()</c>. The return value determines whether the entity is updated
        /// </summary>
        /// <remarks>
        /// <list>
        /// <item>
        /// an array of property indicies - the entity is dirty
        /// </item>
        /// <item>
        /// an empty array - the entity is not dirty
        /// </item>
        /// <item>
        /// <see langword="null"/> - use Hibernate's default dirty-checking algorithm
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="entity">A persistent entity</param><param name="currentState"/><param name="id"/><param name="previousState"/><param name="propertyNames"/><param name="types"/>
        /// <returns>
        /// An array of dirty property indicies or <see langword="null"/> to choose default behavior
        /// </returns>
        public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instantiate the entity class. Return <see langword="null"/> to indicate that Hibernate should use the default
        ///             constructor of the class
        /// </summary>
        /// <param name="entityName">the name of the entity </param><param name="entityMode">The type of entity instance to be returned. </param><param name="id">the identifier of the new instance </param>
        /// <returns>
        /// An instance of the class, or <see langword="null"/> to choose default behaviour
        /// </returns>
        /// <remarks>
        /// The identifier property of the returned instance
        ///             should be initialized with the given identifier.
        /// </remarks>
        public object Instantiate(string entityName, EntityMode entityMode, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the entity name for a persistent or transient instance
        /// </summary>
        /// <param name="entity">an entity instance </param>
        /// <returns>
        /// the name of the entity 
        /// </returns>
        public string GetEntityName(object entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a fully loaded entity instance that is cached externally
        /// </summary>
        /// <param name="entityName">the name of the entity </param><param name="id">the instance identifier </param>
        /// <returns>
        /// a fully initialized entity 
        /// </returns>
        public object GetEntity(string entityName, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when a NHibernate transaction is begun via the NHibernate <see cref="T:NHibernate.ITransaction"/>
        ///             API. Will not be called if transactions are being controlled via some other mechanism.
        /// </summary>
        public void AfterTransactionBegin(ITransaction tx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called before a transaction is committed (but not before rollback).
        /// </summary>
        public void BeforeTransactionCompletion(ITransaction tx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called after a transaction is committed or rolled back.
        /// </summary>
        public void AfterTransactionCompletion(ITransaction tx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when sql string is being prepared. 
        /// </summary>
        /// <param name="sql">sql to be prepared </param>
        /// <returns>
        /// original or modified sql 
        /// </returns>
        public SqlString OnPrepareStatement(SqlString sql)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when a session-scoped (and <b>only</b> session scoped) interceptor is attached
        ///             to a session
        /// </summary>
        /// <remarks>
        /// session-scoped-interceptor is an instance of the interceptor used only for one session.
        ///             The use of singleton-interceptor may cause problems in multi-thread scenario. 
        /// </remarks>
        /// <seealso cref="M:NHibernate.ISessionFactory.OpenSession(NHibernate.IInterceptor)"/><seealso cref="M:NHibernate.ISessionFactory.OpenSession(System.Data.IDbConnection,NHibernate.IInterceptor)"/>
        public void SetSession(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
