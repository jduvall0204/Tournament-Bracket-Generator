﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class RoundsSqlDAO : IRoundsDAO
    {
        private string connectionString { get; }

        public RoundsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public List<Rounds> roundsByMatch {get; set;}
        public Rounds AddRound(Rounds round)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlStatement = "Insert into rounds(roundnumber, tournament_id) " +
                        "VALUES(@roundnumber, @tournament_id);select scope_identity();";
                    SqlCommand cmd = new SqlCommand(sqlStatement, conn);
                    cmd.Parameters.AddWithValue("@roundnumber", round.RoundNumber);
                    cmd.Parameters.AddWithValue("@tournament_id", round.TournamentId);
                    round.RoundId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return round;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Rounds> GetAllRounds()
        {
            List<Rounds> returnRounds = new List<Rounds>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlStatement = "SELECT roundnumber, isActive, tournament_id, round_id FROM rounds";
                    SqlCommand cmd = new SqlCommand(sqlStatement, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Rounds r = GetRoundFromReader(reader);
                            returnRounds.Add(r);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return returnRounds;
        }

        public Rounds GetRound(int roundId)
        {
            Rounds returnRound = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlStatment = "SELECT roundnumber, isActive, tournament_id, round_id FROM rounds WHERE round_id = @round_id";
                    SqlCommand cmd = new SqlCommand(sqlStatment, conn);
                    cmd.Parameters.AddWithValue("@round_id", roundId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnRound = GetRoundFromReader(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return returnRound;
        }

        public bool MarkRoundsInactive(int roundId)
        {
            GetMatchesByRound(roundId);
            int matchesActive = 0;
            foreach(Rounds round in roundsByMatch)
            {
                if(round.IsActive)
                {
                    matchesActive ++;
                }
            }

            if(matchesActive == 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string sqlStatment = "UPDATE round SET isActive = 0 WHERE round_id = @round_id";
                        SqlCommand cmd = new SqlCommand(sqlStatment, conn);
                        cmd.Parameters.AddWithValue("@round_id", roundId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return (rowsAffected > 0);
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return false;
            
            
        }

        public void GetMatchesByRound(int roundId)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlStatement = "SELECT r.roundnumber, r.isActive, m.isActive, m.matchnumber, r.round_id, m.round_id, FROM rounds as r" +
                        "join match as m on r.round_id = m.round_id" +
                        "where m.round_id = @round_id";
                    SqlCommand cmd = new SqlCommand(sqlStatement, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Rounds r = GetRoundFromReader(reader);
                            roundsByMatch.Add(r);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }

        public List<Rounds>GetRoundsByTournamentID( List<int> tournamentIDs,string connectionString)
        {
            
            List<Rounds> output = new List<Rounds>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    
                    foreach (int i in tournamentIDs)
                    {
                        conn.Open();


                        string sqlStatement = $"select * from rounds where tournament_id = {i}";
                        SqlCommand roundCmd = new SqlCommand(sqlStatement, conn);

                        SqlDataReader reader = roundCmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Rounds r = GetRoundFromReader(reader);
                                output.Add(r);
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception e )
            {

                throw ;
            }

            
            return output;
        }

        public Rounds GetRoundFromReader(SqlDataReader reader)
        {
            Rounds r = new Rounds()
            {
                RoundNumber = Convert.ToInt32(reader["roundnumber"]),
                IsActive = Convert.ToBoolean(reader["isActive"]),
                TournamentId = Convert.ToInt32(reader["tournament_id"]),
                RoundId = Convert.ToInt32(reader["round_id"]),
                
            };

            return r;
        }
    }
}
