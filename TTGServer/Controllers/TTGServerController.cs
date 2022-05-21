using Microsoft.AspNetCore.Mvc;
using TTGServer.Models.ServiceModels;
using TTGServer.Services;

namespace TTGServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TTGServerController : ControllerBase
    {
        /*
     * Получить профиль пользователя (авторизация)
     *** Получить профиль пользователя
     *** Регистрация
     * Получить инфу по маршруту (цена, среднее время, список остановок, список машин)
     *** Получить список имен всех маршрутов
     * Отметить остановку для пассажира
     * Очистить остановку
     *** Получить список активных авто (для обновления)
     * Изменить профиль
     * Изменить статус авто
     *** Изменить mapInfo для авто
     * Начать смену
     * Закончить смену
     * Начать рейс
     * Закончить рейс
     * Изменить список остановок
     * Удалить водителя
     * Добавить водителя
     * Изменить водителя
     * Удалить маршрут
     * Добавить маршрут
     * Изменить маршрут
     * Отчет (описание в телеге)
     */
        [HttpGet(@"InfoService/GetProfile/{category}&{login}")]
        public IProfileInfo? GetProfile(char category, string login)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            return service.GetProfileInfo(category, login);
        }

        [HttpGet(@"MapInfoService/GetActiveCars/{wayName}")]
        public IEnumerable<UnitMapInfo> GetActiveCars(string wayName)
        {
            MapInfoService service = new MapInfoService(new TTG_ver3Context());

            return service.GetUnitsMapInfo(wayName);
        }

        [HttpPut(@"MapInfoService/UpdateCarMapInfo")]
        public void UpdateCarMapInfo([FromBody] UnitMapInfo unitMapInfo)
        {
            MapInfoService service = new MapInfoService(new TTG_ver3Context());

            service.UpdateUnitMapInfo(unitMapInfo);
        }

        [HttpPost(@"Authorization/RegisterPassenger")]
        public void RegisterPassenger([FromBody] PassengerRegistration passenger)
        {
            AuthorizationService service = new AuthorizationService(new TTG_ver3Context());

            service.RegisterPassenger(passenger);
        }

        [HttpPost(@"Authorization/RegisterOwner")]
        public void RegisterOwner([FromBody] OwnerRegistration owner)
        {
            AuthorizationService service = new AuthorizationService(new TTG_ver3Context());

            service.RegisterOwner(owner);
        }

        [HttpPost(@"Authorization/RegisterUnit")]
        public void RegisterUnit([FromBody] UnitRegistration unit)
        {
            AuthorizationService service = new AuthorizationService(new TTG_ver3Context());

            service.RegisterUnit(unit);
        }

        [HttpGet(@"InfoService/GetAllWayNames")]
        public string[] GetAllWayNames()
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            return service.GetAllWayNames();
        }
    }
}