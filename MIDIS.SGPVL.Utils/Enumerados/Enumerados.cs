namespace MIDIS.SGPVL.Utils.Enumerados
{
    
    public enum EnumeradoCabecera
    {

        TIPO_DOCUMENTO = 1,
        TIPO_VIA = 2,
        TIPO_ZONA = 3,
        ESTADO_CIVIL = 4,
        MEDIO_CONTACTO = 5,
        TIPO_PERSONA = 6,
        GENERO = 7,
        TIPO_MONEDA = 8,
        SITUACION_GENERAL = 9,
        TIPO_RESOLUCION = 10,
        TIPO_CARGOS = 11,
        TIPO_OSB = 12,
        TIPO_ALIMENTO = 13,
        TIPO_SOCIO = 14,
        TIPO_CLASIFICACION = 15,
        TIPO_CLASIFICACION_SOCIO_ECONOMICA = 16,
        TIPO_CARGO_JD = 17
    }

    public enum EnumTipoDocumento
    {
        dni = 1,
        LM = 2,
        brevete = 3,
        ruc = 4,
        pasaporte = 7,
        ce= 11
    }

    public enum EnumEstadoCivil
    {
        soltero = 36,
        casado = 37,
        viudo = 38,
    }

    public enum EnumMeidioContacto
    {
        CORREO = 39,
        TELEFONO = 40,
        CELULAR = 41,
        FAX = 42,
        PAGINA_WEB = 43,
    }


    public enum EnumTipoPersona
    {
        natural = 44,
        juridica = 45
    }



    public enum EnumSexo
    {
        masculino = 46,
        femenino = 47
    }

    public enum EnumPrioridad
    {
        PRIORIDAD_1=72	,
        PRIORIDAD_2= 73
    }



}
