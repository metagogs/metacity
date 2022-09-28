package main

import (
	"github.com/metagogs/gogs"
	"github.com/metagogs/gogs/acceptor"
	"github.com/metagogs/gogs/config"
	"github.com/metagogs/metacity/internal/server"
	"github.com/metagogs/metacity/internal/svc"
	"github.com/metagogs/metacity/model"
)

func main() {

	config := config.NewConfig()

	app := gogs.NewApp(config)
	app.AddAcceptor(acceptor.NewWSAcceptror(&acceptor.AcceptroConfig{
		HttpPort: 8888,
		Name:     "base",
		Groups: []*acceptor.AcceptorGroupConfig{
			&acceptor.AcceptorGroupConfig{
				GroupName: "base",
			},
		},
	}))

	ctx := svc.NewServiceContext(app)
	srv := server.NewServer(ctx)

	model.RegisterAllComponents(app, srv)

	defer app.Shutdown()
	app.Start()
}
