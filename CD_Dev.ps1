

Write-Information('---step4---')

docker build -f src/AZ_SSOTest.Web/Dockerfile -t ic/leave-vnext .

Write-Information('---step5---')
docker tag ic/leave-vnext:latest registry.ic-digital.cn/temp/ic/leave-vnext
docker push registry.ic-digital.cn/temp/ic/leave-vnext

Write-Information('---step6---')
docker -H tcp://192.168.100.111:2375 info

Write-Information('---step7---')
docker -H tcp://192.168.100.111:2375 pull registry.ic-digital.cn/temp/ic/leave-vnext

Write-Information('---step8---')
docker -H tcp://192.168.100.111:2375 stop leave-vnext
docker -H tcp://192.168.100.111:2375 rm leave-vnext

docker -H tcp://192.168.100.111:2375 run --name leave-vnext -d -p 80:80  -v C:\ICLeaveVnext:/app/wwwroot/host -v C:\ICLeaveVnext\Logs:/app/Logs  --env-file dev.env  registry.ic-digital.cn/temp/ic/leave-vnext 



