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
     * �������� ������� ������������ (�����������)
     *** �������� ������� ������������
     *** �����������
     * �������� ���� �� �������� (����, ������� �����, ������ ���������, ������ �����)
     *** �������� ������ ���� ���� ���������
     * �������� ��������� ��� ���������
     * �������� ���������
     *** �������� ������ �������� ���� (��� ����������)
     * �������� �������
     * �������� ������ ����
     *** �������� mapInfo ��� ����
     * ������ �����
     * ��������� �����
     * ������ ����
     * ��������� ����
     * �������� ������ ���������
     * ������� ��������
     * �������� ��������
     * �������� ��������
     * ������� �������
     * �������� �������
     * �������� �������
     * ����� (�������� � ������)
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