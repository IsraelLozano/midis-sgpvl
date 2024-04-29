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
        TIPO_CLASIFICACION_SOCIO_ECONOMICA = 16
    }

    public enum EnumTipoDocumento
    {
        dni = 1,
        LM = 2,
        brevete = 3,
        ruc = 4,
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



    public enum EnumTipoSucursal
    {
        tienda = 63,
        cd = 64
    }

    public enum EnumTipoEmpresa
    {
        hpsa = 65,
        hpo = 66
    }
    public enum EnumTipoViaje
    {
        interprovincial = 67,
        local = 68
    }

    public enum EnumTIPO_MODALIDAD
    {
        DE_MES_VENCIDO = 62,
        QUINCENAL = 61,
        semanal = 60
    }

    public enum EnumTIPO_MONEDA
    {
        dolares = 58,
        soles = 59
    }

    public enum EnumSITUACION_GENERAL
    {
        ABIERTO = 69,
        PENDIENTE = 70,
        CERRADO = 71,
        ACTIVO = 72,
        APROBADO= 73,
        PROCESO= 74
    }

    public enum EnumESTADO_VIAJE
    {
        PROGRAMADO = 75,
        CANCELADO = 76,
        EN_RUTA = 77,
        RECIBIDO = 78,
        CARGADO = 79,
        ANULADO = 80

    }

    public enum EnumSITUACION_OC
    {
        PENDIENTE_DE_OC_SAP = 81,
        OC_SAP = 82,

    }

    public enum EnumSITUACION_certi_NIVELES
    {
        APTO = 83,
        RECHAZADO = 84,

    }


    public enum EnumRoles
    {
        ADMINSTRADOR_SISTEMAS = 1,
        ADMINISTRADOR_CONTRATOS_TARIFAS = 2,
        JEFE_TRANSPORTES = 3,
        ANALISTA_PROGRAMADOR = 4,
        OPERACIONES_LOGISTICAS = 5,
        SOPORTE_SISTEMAS = 6,
    }

    public enum EnumOpcionesSistemas
    {
        Opciones_del_Sistema = 1,
        Inicio = 2,
        Seguridad = 3,
        Maestros = 4,
        Contratos = 5,
        Tarifarios = 6,
        Programacion = 7,
        Liquidaciones = 8,
        Reportes = 9,
        Usuarios = 10,
        Roles = 11,
        Transportistas = 12,
        Tipos_de_UT = 13,
        Provincias = 14,
        Distritos = 15,
        Departamentos = 16,
        Sucursales = 17,
        Periodos = 18,
        Adicional = 19,
        Certificado = 20,
        Distancias = 21,
        Registro_Contratos = 22,
        Registro_Tarifarios = 23,
        Viajes = 24,
        Cargas = 25,
        Descargas = 26,
        OCs = 27,
        Reporte_de_Costos = 28,
        Reporte_de_Presupuestos = 29,
        Auditoria = 30,
        Logs = 31,
        Presupuesto = 32,
        Region= 33,
        producto= 34


    }

}
