namespace Effectus.Infrastructure
{
	using System;
	using NHibernate;

	public class DataBindingIntercepter : EmptyInterceptor
	{
		public ISessionFactory SessionFactory { set; get; }

		public override object Instantiate(string clazz, object id)
		{

			Type type = Type.GetType(clazz);
			if (type != null)
			{
				var instance = DataBindingFactory.Create(type);
				SessionFactory.GetClassMetadata(clazz).SetIdentifier(instance, id);
				return instance;
			}

			return base.Instantiate(clazz, id);
		}

		public override string GetEntityName(object entity)
		{
			var markerInterface = entity as DataBindingFactory.IMarkerInterface;
			if (markerInterface != null)
				return markerInterface.TypeName;
			return base.GetEntityName(entity);
		}
	}
}