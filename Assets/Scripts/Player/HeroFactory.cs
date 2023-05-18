using Zenject;
using UnityEngine;
using Assets.Scripts.Input_System;

namespace Assets.Scripts.Player
{
    public class HeroFactory
    {
        private readonly DiContainer container;
        private readonly Hero heroPref;

        [Inject]
        public HeroFactory(DiContainer container,
                           InputService inputService)
        {
            this.container = container;
        }

        public void Create(Vector3 position)
        {
            var hero = container.InstantiatePrefabForComponent<Hero>(heroPref);
            hero.SetComponentsData();
            hero.Initialize();
        }
    }
}
