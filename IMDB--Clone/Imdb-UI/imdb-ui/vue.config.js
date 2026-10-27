const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: [
    'vuetify'
  ]
})
module.exports = {
  // options...
  devServer: {
        proxy: 'http://localhost:8081/movies',
    }
}

