import { createStore } from 'vuex'
import SearchHistoryEntry from './SearchHistoryEntry'

export default createStore({
  state: {
    searchHistory: Array<SearchHistoryEntry>(),
  },
  mutations: {
    addSearchHistoryEntry(state, searchHistoryEntry: SearchHistoryEntry) {
      state.searchHistory.push(searchHistoryEntry);
    }
  },
  actions: {    
    addSearchHistoryEntry(context, searchHistoryEntry: SearchHistoryEntry) {
      context.commit("addSearchHistoryEntry", searchHistoryEntry);
    }
  },
  modules: {
  }
})
