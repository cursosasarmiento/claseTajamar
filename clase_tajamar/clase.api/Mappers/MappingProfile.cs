using AutoMapper;
using clase.api.Models;
using clase.api.Models.DTOs.MascotaDtos;
using clase.api.Models.DTOs.MascotaTipoDto;
using clase.api.Models.DTOs.PersonaDtos;

namespace clase.api.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            #region Persona
            CreateMap<Persona, PersonaCreateRequestDto>();
            CreateMap<PersonaCreateRequestDto, Persona>();

            CreateMap<Persona, PersonaUpdateRequestDto>();
            CreateMap<PersonaUpdateRequestDto, Persona>();

            CreateMap<Persona, PersonaSimpleResponseDto>();
            CreateMap<PersonaSimpleResponseDto, Persona>();

            CreateMap<Persona, PersonaFullResponseDto>();
            CreateMap<PersonaFullResponseDto, Persona>();
            #endregion Persona

            #region Mascota

            CreateMap<Mascota, MascotaCreateRequestDto>();
            CreateMap<MascotaCreateRequestDto, Mascota>();

            CreateMap<Mascota, MascotaUpdateRequestDto>();
            CreateMap<MascotaUpdateRequestDto, Mascota>();

            CreateMap<Mascota, MascotaSimpleResponseDto>();
            CreateMap<MascotaSimpleResponseDto, Mascota>();

            CreateMap<Mascota, MascotaFullResponseDto>();
            CreateMap<MascotaFullResponseDto, Mascota>();

            #endregion Mascota

            #region MascotaTipo

            CreateMap<MascotaTipo, MascotaTipoCreateRequestDto>();
            CreateMap<MascotaTipoCreateRequestDto, MascotaTipo>();

            CreateMap<MascotaTipo, MascotaTipoFullResponseDto>();
            CreateMap<MascotaTipoFullResponseDto, MascotaTipo>();
            #endregion MascotaTipo

        }
    }
}
