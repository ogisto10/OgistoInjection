using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace OgistoInjection;

public static class ServiceDI
{
    public static void AddAutoDiServices(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<InjectableAttribute>() != null);

        foreach (var type in typesWithAttribute)
        {
            var attr = type.GetCustomAttribute<InjectableAttribute>();
            var interfaceTypes = type.GetInterfaces();

            if (!interfaceTypes.Any())
            {
                Register(services, attr!.Type, type, null);
                continue;
            }

            foreach (var interfaceType in interfaceTypes)
            {
                Register(services, attr!.Type, type, interfaceType);
            }
        }
    }

    private static void Register(IServiceCollection services, DITypes type, Type impl, Type? abstraction)
    {
        switch (type)
        {
            case DITypes.Scoped:
                if (abstraction != null)
                    services.AddScoped(abstraction, impl);
                else
                    services.AddScoped(impl);
                break;
            case DITypes.Singleton:
                if (abstraction != null)
                    services.AddSingleton(abstraction, impl);
                else
                    services.AddSingleton(impl);
                break;
            case DITypes.Transient:
                if (abstraction != null)
                    services.AddTransient(abstraction, impl);
                else
                    services.AddTransient(impl);
                break;
        }

        Console.WriteLine($"Registered {(abstraction?.Name ?? impl.Name)} as {type} -> {impl.Name}");
    }
}
