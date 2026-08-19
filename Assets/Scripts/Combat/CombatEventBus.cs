using System;
using System.Collections.Generic;

namespace NAPI.Combat
{
    /// <summary>
    /// Bus de eventos de combate. Pensado para existir uno por batalla
    /// (su creación/propiedad por parte de BattleManager se resolverá en
    /// una etapa posterior; esta clase no asume nada sobre eso).
    ///
    /// Responsabilidad única: registrar listeners y notificarlos cuando
    /// se publica un evento. No sabe qué significan los eventos, no
    /// conoce Ultimate, CAR, elementos, turnos, daño, UI ni ningún otro
    /// sistema de gameplay. Nada llama todavía a Publish() desde el
    /// resto del proyecto: esa conexión corresponde a etapas futuras.
    /// </summary>
    public class CombatEventBus
    {
        private readonly Dictionary<Type, Delegate> listeners = new();

        public void Subscribe<TEvent>(Action<TEvent> listener)
        {
            Type eventType = typeof(TEvent);

            if (listeners.TryGetValue(eventType, out Delegate existing))
                listeners[eventType] = Delegate.Combine(existing, listener);
            else
                listeners[eventType] = listener;
        }

        public void Unsubscribe<TEvent>(Action<TEvent> listener)
        {
            Type eventType = typeof(TEvent);

            if (!listeners.TryGetValue(eventType, out Delegate existing))
                return;

            Delegate combined = Delegate.Remove(existing, listener);

            if (combined == null)
                listeners.Remove(eventType);
            else
                listeners[eventType] = combined;
        }

        public void Publish<TEvent>(TEvent combatEvent)
        {
            if (!listeners.TryGetValue(typeof(TEvent), out Delegate existing))
                return;

            ((Action<TEvent>)existing).Invoke(combatEvent);
        }
    }
}
