using System.Reflection;
using AutoMapper;

namespace Application.Common.Mappings;

public sealed class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile() => ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);
        const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
        var argumentTypes = new Type[] { typeof(Profile) };
        
    
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

            if (interfaces.Count == 0) continue;
            foreach (var interfaceMethodInfo in interfaces
                         .Select(@interface => @interface.GetMethod(mappingMethodName, argumentTypes)))
            {
                interfaceMethodInfo?.Invoke(instance, new object[] { this });
            }
        }

        return;

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
    }
}