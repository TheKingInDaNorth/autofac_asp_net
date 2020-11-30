using Lib.Abstractions;
using System;
using System.Collections.Generic;

namespace Lib
{
    public class SuperheroService
    {
        IAvengerRepository _avengerRepository;
        ILogger _logger;

        public SuperheroService(IAvengerRepository avengerRepository, ILogger logger)
        {
            _avengerRepository = avengerRepository;
            _logger = logger;
        }
        public IEnumerable<Hero> GetAvengers()
        {
            _logger.Log("Calling SuperheroService.GetAvengers.");

            var avengers = _avengerRepository.FetchAll();

            _logger.Log("SuperheroService.GetAvengers called.");

            return avengers;
        }

        public Hero GetAvenger(string name)
        {
            _logger.Log("Calling SuperheroService.GetAvenger('{0}').", name);

            var avenger = _avengerRepository.Fetch(name);

            _logger.Log("SuperheroService.GetAvenger('{0}') called.", name);

            return avenger;
        }
    }
}
