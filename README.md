# sii-autenticacion

El repositorio contiene un simple proyecto de biblioteca de clases .NET CORE 2.1

El propósito de esta biblioteca es realizar el proceso de autenticación automática que proporciona el <strong>Servicio de Impuestos Internos</strong> en Chile (conocido también por su sigla <strong>SII</strong>).
La clase principal, <strong>Autenticacion</strong> (Autenticacion.cs), tiene 2 métodos públicos: GetSemillaAsync y GetTokenAsync.

El método <strong>GetSemillaAsync</strong> permite obtener una semilla desde un servicio web del SII, mientras que el método <strong>GetTokenAsync</strong> recibe como parámetro una semilla válida, la firma con el certificado digital del emisor (El cual debe coincidir con el registrado por el SII) y la envía al servicio web correspondiente para obtener un token de autenticación.

<strong>Glosario</strong>
<ul>
<li><strong>Semilla</strong>: Número único y aleatorio que sirve como identificador para la sesión de un cliente y que tiene un timeout de 2 (dos) minutos.</li>

<li><strong>Token</strong>: Identificador único el cual es almacenado y enviado al cliente en el Body (Cuerpo) del Response.</li>
</ul>
