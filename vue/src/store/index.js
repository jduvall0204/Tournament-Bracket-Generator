import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));
const currentTournament = JSON.parse(localStorage.getItem('tournament'));
const Participants = localStorage.getItem('Participants')
const currentTeams = localStorage.getItem('teams');
const currentRoundsList = localStorage.getItem('roundsList');
const currentMatches = localStorage.getItem('matchArray');



if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
   
    Participants: Participants || [],
    ActiveBracket:{
      name:'',
      teams: currentTeams|| [],
    },
    matches:currentMatches || [{}],
    Tournament:currentTournament || {},
    roundsList:currentRoundsList || [{}],
    MyTournaments:[],
    match: {
      MatchNumber: 0,
      isActive: true,
      scoreTeam1: 0,
      scoreTeam2: 0,
      team1winner: false,
      team2winner: false,
      roundId: 0,
      matchId: 0,
      Participants1_Id:0,
      Participants1_name:'',
      PArticipants1_isActive:true, 
      Participants2_Id:0,
      Participants2_name:'',
      Participants2_isActive:true,
      

    }
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      localStorage.removeItem('tournament');
      state.token = '';
      state.user = {};
      state.tournament={};
      axios.defaults.headers.common = {};
    },
    UPDATE_PARTICIPANTS(state,numberOfPants){
      if( numberOfPants == 0){
        state.Participants = []; 
      } else {
        for(let i = 0; i < numberOfPants ; i++){
          let Team = {
            id: "Team "+ (i+1),
            name:"",
            edit:false
          };
          state.Participants.push(Team);
        }
      }
    },
    SET_BRACKET_NAME(state,name){
      state.ActiveBracket.name = name;
    },
    SET_PARTICIPANTS(state,teams){
      state.ActiveBracket.teams=teams;
      localStorage.setItem('teams',JSON.stringify(teams))
    },
    SET_TOURNAMENT(state,tournament){
      state.Tournament=tournament;
      localStorage.setItem('tournament',JSON.stringify(tournament));
    },
    SET_ROUNDS_LIST(state,roundsArray){
      state.roundsList=roundsArray;
      localStorage.setItem('roundsList',JSON.stringify(roundsArray));
    },
    SET_MATCHES(state,matchArray){
      state.matches=matchArray;
      localStorage.setItem('matchArray',JSON.stringify(matchArray));

    },
    SET_MY_TOURNAMENTS(state,TournamentsList){
      state.MyTournaments= TournamentsList;
    }
  }
})
