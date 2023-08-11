<h1 align="center">Jellyfin</h1>
<h3 align="center">The Free Software Media System</h3>

<p align="center">
<img alt="Logo Banner" src="https://raw.githubusercontent.com/jellyfin/jellyfin-ux/master/branding/SVG/banner-logo-solid.svg?sanitize=true"/>
<img alt="Logo" src="/oncloudlogo.png"/>
</p>

# [Cloud Treinamentos](https://comunidadecloud.com/)
### Oficina de Projetos 12
### Grupo 1 - OnCloud

## Colaborador Principal
- Heitor Lourenço Silva

### Demais Colaboradores
- Emanuel Henrique B Silva
- Felipe Bezares
- Heverton Luiz Fornazari
- Jony dos Santos Barbosa
- Manoel Domingues
- Rafael Vieira de Souza
- Ramon Alberto Lima Cruz
- Samuel Gonçalves
- Sérgio Fuzimoto
- Tárik Pomim
- Tercio Nascimento Caciano

## Alterações feitas
- Docker file retirado parametros do entrypoint para poder usar as envoriment variables
- Trocado Entity Framework SQLite para MySQL
- Recriado Migrations
- Adicionado a variavel de ambiente JELLYFIN_CONN_STR

## Utilizaçao via docker
#### Link do Doker Hub
https://hub.docker.com/r/heitorlourencosilva/jellyfin_mysql

#### docker-compose.yml
```bash
services:
  jellyfin:
    image: heitorlourencosilva/jellyfin_mysql
    # user: ubuntu:ubuntu # user:group / acredito que nao precisa dessa configuracao pode fechar
    # network_mode: 'host' # Se ativado ignora a ports e joga no porta 8096 / 8920
    restart: 'unless-stopped'
    environment: # Configura as variaveis das pastas / o que vai aparecer no jellyfin
      - JELLYFIN_CACHE_DIR=/var/cache/jellyfin
      - JELLYFIN_CONFIG_DIR=/etc/jellyfin 
      - JELLYFIN_DATA_DIR=/var/lib/jellyfin
      - JELLYFIN_LOG_DIR=/var/log/jellyfin
      - JELLYFIN_CONN_STR=server=host_endpoint;user=usuario;password=senha;database=nome_da_base
    volumes: # Mapaia para a pasta /root/jellyfin
      - /home/ubuntu/efs-jellyfin/config:/etc/jellyfin
      - /home/ubuntu/efs-jellyfin/cache:/var/cache/jellyfin
      - /home/ubuntu/efs-jellyfin/data:/var/lib/jellyfin
      - /home/ubuntu/efs-jellyfin/log:/var/log/jellyfin
      - /home/ubuntu/efs-jellyfin/media:/var/media/jellyfin
    ports:
      - "80:8096" # Joga na porta 80
      - "443:8920" # Joga na porta 443
```