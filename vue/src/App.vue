<template>
  <div id="app" class="layout">
    <!-- <div id="nav" class="header-stripe">
      <router-link class="navigation" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link class="navigation" v-bind:to="{ name:'myBrackets'}">My Tournaments</router-link>&nbsp;|&nbsp;
      <router-link class="navigation" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link class="navigation" v-bind:to="{ name: 'login' }" v-else>Login</router-link>

    </div> -->
    <p id="nav" class="header-stripe" style="color:#050E28">
      <router-link class="navigation-link" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <!-- below is for the brackets link, active and unactive tournaments -->
      <!-- <router-link class="navigation-link" v-bind:to="{name: ''}">Brackets</router-link>&nbsp;|&nbsp;
      <router-link class="navigation-link" v-bind:to="{name: ''}"> Active Tournaments</router-link>&nbsp;|&nbsp;
      <router-link class="navigation-link" v-bind:to="{name: ''}"> Completed Tournaments</router-link>&nbsp;|&nbsp; -->
      
      <router-link class="navigation-link" v-bind:to="{ name:'myBrackets'}">My Tournaments</router-link>&nbsp;|&nbsp;
      
      <!-- v-if and v-else for login/logout links -->
      <router-link class="navigation-link" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link class="navigation-link" v-bind:to="{ name: 'login' }" v-else>Login</router-link>
    </p>
    <router-view class="main"/>
    <div class="footer-stripe">
      <footer>
        <p style="color: #E5E5E5;"> &copy; Bracketeers 2020 </p>
      </footer>
    </div>
  </div>
</template>

<style scoped>
.navigation-link {
    padding: 0.5em;
    color:#050E28;
    text-decoration: none;
}

.navigation-link:hover {
    color: #FCA311;
    text-decoration: underline;
}

.header-stripe {
  background-color: #7C99BC;
  padding: 1em;
  width: 100%;
}

.footer-stripe {
  background-color: #3a506b;
  padding: 1em;
  width: 100%;
  height: 55px;
  /* bottom: 0; */
}

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.main {
  flex: 1;
}
</style>