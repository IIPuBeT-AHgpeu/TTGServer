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
     *** Отметить остановку для пассажира
     *** Отменить выбор остановки для пассажира
     * Очистить остановку
     *** Получить список активных авто (для обновления)
     * Изменить профиль
     *** Изменить статус авто
     *** Изменить mapInfo для авто
     *** Начать смену
     *** Закончить смену
     *** Проверить наличие начатой смены
     *** Начать рейс
     *** Закончить рейс
     * Изменить список остановок
     * Удалить водителя
     *** Добавить водителя
     * Изменить водителя
     *** Удалить маршрут
     *** Добавить маршрут
     *** Изменить маршрут
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

        [HttpPut(@"InfoService/UpdatePassengerPosition")]
        public void UpdatePassengerPosition([FromBody] PassengerWaiting passengerWaiting)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.UpdatePassengerPosition(passengerWaiting);
        }

        [HttpPost(@"InfoService/CreateWay")]
        public void CreateWay([FromBody] WayModel way)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.AddNewWay(way);
        }

        [HttpPut(@"InfoService/UpdateWay/{oldWayName}")]
        public void UpdateWay(string oldWayName, [FromBody] WayModel way)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.UpdateWay(oldWayName, way);
        }

        [HttpDelete(@"InfoService/DeleteWay/{wayName}&{ownerLogin}")]
        public void DeleteWay(string wayName, string ownerLogin)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.DeleteWay(wayName, ownerLogin);
        }

        [HttpPut(@"InfoService/UpdateCarStatus")]
        public void UpdateCarStatus([FromBody] StatusModel newStatus)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.ChangeUnitStatus(newStatus.Login, newStatus.Status);
        }

        [HttpPost(@"InfoService/StartWorkDay")]
        public void StartWorkDay([FromBody] StartWorkDayModel workday)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.SetStartWorkDay(workday);
        }

        [HttpPut(@"InfoService/FinishWorkDay")]
        public void FinishWorkDay([FromBody] EndWorkDayModel workday)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.SetFinishWorkDay(workday);
        }

        [HttpGet(@"InfoService/ActiveWorkDayIsExist/{login}")]
        public bool ActiveWorkDayIsExist(string login)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            return service.IsWorkDayExists(login);
        }

        [HttpPost(@"InfoService/StartTrip")]
        public void StartTrip([FromBody] StartTripModel trip)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.SetStartTrip(trip);
        }

        [HttpPut(@"InfoService/FinishTrip")]
        public void FinishTrip([FromBody] EndTripModel trip)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            service.SetFinishTrip(trip);
        }

        [HttpGet(@"InfoService/ActiveTripExist/{login}")]
        public bool ActiveTripExist(string login)
        {
            InfoService service = new InfoService(new TTG_ver3Context());

            return service.IsStartTripExists(login);
        }
    }
}