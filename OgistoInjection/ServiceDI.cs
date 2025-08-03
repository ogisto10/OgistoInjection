using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace OgistoInjection;

public static class ServiceDI
{
    public static void AddAutoDiServices(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var classWithAttribute = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<InjectableAttribute>() != null);

        foreach (var classType in classWithAttribute)
        {
            var attr = classType.GetCustomAttribute<InjectableAttribute>();
            var interfaceTypes = classType.GetInterfaces();

            if (!interfaceTypes.Any())
            {
                Register(services, attr!.Type, classType, null);
                continue;
            }

            foreach (var interfaceType in interfaceTypes)
            {
                Register(services, attr!.Type, classType, interfaceType);
            }
        }
    }

    private static void Register(IServiceCollection services, DITypes type, Type classType, Type? interfaceType)
    {
        switch (type)
        {
            case DITypes.Scoped:
                if (interfaceType != null)
                    services.AddScoped(interfaceType, classType);
                else
                    services.AddScoped(classType);
                break;
            case DITypes.Singleton:
                if (interfaceType != null)
                    services.AddSingleton(interfaceType, classType);
                else
                    services.AddSingleton(classType);
                break;
            case DITypes.Transient:
                if (interfaceType != null)
                    services.AddTransient(interfaceType, classType);
                else
                    services.AddTransient(classType);
                break;
        }
    }
}
