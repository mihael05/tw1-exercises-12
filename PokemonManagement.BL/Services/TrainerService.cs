using PokemonManagement.BL.Exceptions;
using PokemonManagement.BL.Interfaces;
using PokemonManagement.DAL.Models;
using PokemonManagement.DAL.Repositories;

namespace PokemonManagement.BL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerPokemonRepository _repository;

        public TrainerService(ITrainerPokemonRepository repository)
        {
            _repository = repository;
        }

        public void Train(int trainerPokemonId)
        {
            // 1 retrieve ownedpokemon by trainerPokemonId
            var ownedPokemon = _repository.GetBy(trainerPokemonId);
            if (ownedPokemon is null) 
                throw new EntityNotFoundException();

            // 2 check valid level
            if (ownedPokemon.Level >= 100)
                throw new PokemonLogicException("max level reached");

            // 3 increase level and update entity
            ownedPokemon.Level += 1;
            _repository.Update(ownedPokemon);
        }

        public void Evolve(TrainerPokemon ownedPokemon)
        {
            if (ownedPokemon.Pokemon?.EvolvesToId is null || ownedPokemon.Pokemon.EvolvesTo is null)
            {
                throw new PokemonLogicException("Pokemon cannot evolve");
            }
        }
    }
}
